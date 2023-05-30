using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Dev.Tools.Configs;
using Dev.Tools.Results;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Publication.Rabbit.Subscription.Storage.RmqPublisher.Facade;
using Publication.Rabbit.Subscription.Storage.RmqPublisher.Facade.Args;
using Publication.Rabbit.Subscription.Storage.RmqPublisher.Infra.Http.Dto;
using Publication.Rabbit.Subscription.Storage.RmqPublisher.Infra.Http.Proxy.Mappers;

namespace Publication.Rabbit.Subscription.Storage.RmqPublisher.Infra.Http.Proxy
{
    internal sealed partial class HttpRmqPublisherServiceProxy : IRmqPublisherClient
    {
        //Прокси знает название контроллера
        private const string ControllerName = "rmq-publisher";
        private string AuthToken { get; }
        private readonly HttpClient _httpClient;
        private readonly ILogger<HttpRmqPublisherServiceProxy> _logger;


        public HttpRmqPublisherServiceProxy(
            IOptions<AuthConfig> authConfig,
            IHttpClientFactory httpClientFactory,
            ILogger<HttpRmqPublisherServiceProxy> logger)
        {
            //Прокси сам создает HttpClient, прокси знает имя клиента, потому что сам его и формирует. 
            _httpClient = httpClientFactory.CreateClient(Constants.RmqPublisherHttpClientName);
            AuthToken = authConfig.Value.AuthToken;
            _logger = logger;
        }

        public async Task<ISuccessData> SendData(IPersonArgs args, CancellationToken cancellationToken)
        {
            //Метод прокси знает название метода контроллера
            var methodName = "send-data";
            //Метод SendPostAsync<T> описан в статическом HttpClientExtensions
            var result = await _httpClient.SendPostAsync<PersonDto>(dto: args.ToDto(),
                                                                    controllerName: ControllerName,
                                                                    methodName: methodName,
                                                                    authToken: AuthToken,
                                                                    token: cancellationToken);

            if (result.ErrorData != null)
            {
                _logger.LogError($"Error while sending http request. Reason: {result.ErrorData.ErrorMessage}.");
            }
            return result;
        }
    }
}