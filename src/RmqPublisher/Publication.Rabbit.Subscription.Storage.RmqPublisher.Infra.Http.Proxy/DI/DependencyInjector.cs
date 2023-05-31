using System;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Publication.Rabbit.Subscription.Storage.RmqPublisher.Facade;

namespace Publication.Rabbit.Subscription.Storage.RmqPublisher.Infra.Http.Proxy.DI;

public static class DependencyInjector
{
    public static void AddRmqPublisherClient(this IServiceCollection services, IConfiguration conf)
    {
        services
            .AddSingleton<IRmqPublisherClient, HttpRmqPublisherServiceProxy>()
            .AddHttpClient(
                Constants.RmqPublisherHttpClientName,
                httpClient =>
                {
                    httpClient.BaseAddress = new Uri(conf["RMQ_PUBLISHER_HTTP_CLIENT_BASE_ADDRESS"]);
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", conf["AUTH_TOKEN"]);
                });
    }
}