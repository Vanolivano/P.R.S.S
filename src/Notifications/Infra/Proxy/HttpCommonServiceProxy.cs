using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Dev.Tools.Helpers.Http;
using Dev.Tools.Results;
using Microsoft.Extensions.Logging;
using Publication.Rabbit.Subscription.Storage.Notifications.Facade;

namespace Publication.Rabbit.Subscription.Storage.Notifications.Infra.Proxy;
internal sealed class HttpNotificationClientProxy : INotificationClient
{
    private const string ControllerName = "notifications";
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<HttpNotificationClientProxy> _logger;

    public HttpNotificationClientProxy(IHttpClientFactory httpClientFactory,
                                       ILogger<HttpNotificationClientProxy> logger) =>
        (_httpClientFactory, _logger) = (httpClientFactory, logger);

    public async Task<ISuccessData> PushMessageAsync(string message, CancellationToken cancellationToken)
    {
        //Метод прокси знает название метода контроллера
        var methodName = "push-message";
        //Прокси сам создает HttpClient, прокси знает имя клиента, потому что сам его и формирует. 
        using var httpClient = _httpClientFactory.CreateClient(Constants.NotificationsHttpClientName);
        //Метод SendPostAsync<T> описан в статическом HttpClientExtensions
        var result = await httpClient.SendPostAsync<string>(dto: message,
                                                            controllerName: ControllerName,
                                                            methodName: methodName,
                                                            token: cancellationToken);

        if (result.ErrorData != null)
        {
            _logger.LogError("Error while sending message to notification. " +
                            $"Reason: {result.ErrorData.ErrorMessage}.");
        }
        return result;
    }
}