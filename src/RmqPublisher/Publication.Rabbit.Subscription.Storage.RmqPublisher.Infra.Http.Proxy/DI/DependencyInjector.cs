using System;
using Dev.Tools.Configs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Publication.Rabbit.Subscription.Storage.RmqPublisher.Facade;

namespace Publication.Rabbit.Subscription.Storage.RmqPublisher.Infra.Http.Proxy.DI;

public static class DependencyInjector
{
    public static void AddRmqPublisherClient(this IServiceCollection services, IConfiguration conf)
    {
        services
            .Configure<AuthConfig>(ac => { ac.AuthToken = conf.GetValue<string>("AUTH_TOKEN"); })
            .AddSingleton<IRmqPublisherClient, HttpRmqPublisherServiceProxy>()
            .AddHttpClient(
            Constants.RmqPublisherHttpClientName,
            httpClient =>
            {
                httpClient.BaseAddress = new Uri(conf.GetValue<string>("RMQ_PUBLISHER_HTTP_CLIENT_BASE_ADDRESS"));
            });
    }
}