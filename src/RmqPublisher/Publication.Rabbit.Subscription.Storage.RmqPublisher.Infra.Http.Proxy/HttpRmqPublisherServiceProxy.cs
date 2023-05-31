using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Dev.Tools.Helpers.Http;
using Dev.Tools.Results;
using Microsoft.Extensions.Logging;
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
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<HttpRmqPublisherServiceProxy> _logger;


        public HttpRmqPublisherServiceProxy(IHttpClientFactory httpClientFactory,
                                            ILogger<HttpRmqPublisherServiceProxy> logger) =>
            (_httpClientFactory, _logger) = (httpClientFactory, logger);


        public async Task<ISuccessData> SendData(IPersonArgs args, CancellationToken cancellationToken)
        {
            //Метод прокси знает название метода контроллера
            var methodName = "send-data";
            //Прокси сам создает HttpClient, прокси знает имя клиента, потому что сам его и формирует. 
            using var httpClient = _httpClientFactory.CreateClient(Constants.RmqPublisherHttpClientName);
            //Метод SendPostAsync<T> описан в статическом HttpClientExtensions
            var result = await httpClient.SendPostAsync<PersonDto>(dto: args.ToDto(),
                                                                    controllerName: ControllerName,
                                                                    methodName: methodName,
                                                                    token: cancellationToken);

            if (result.ErrorData != null)
            {
                _logger.LogError($"Error while sending http request. Reason: {result.ErrorData.ErrorMessage}.");
            }
            return result;
        }
    }
}