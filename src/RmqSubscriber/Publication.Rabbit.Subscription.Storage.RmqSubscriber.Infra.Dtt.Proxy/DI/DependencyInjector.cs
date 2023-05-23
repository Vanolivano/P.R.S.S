using Dev.Tools.Configs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Publication.Rabbit.Subscription.Storage.RmqSubscriber.Facade;

namespace Publication.Rabbit.Subscription.Storage.RmqSubscriber.Infra.Dtt.Proxy.DI;

public static class DependencyInjector
{
    public static IServiceCollection AddRmqSubscriberClient(this IServiceCollection services, IConfiguration conf) =>
        services
        .Configure<RabbitConfig>(config => config.ConnectionString = conf.GetValue<string>("RABBIT_MQ_CONNECTION_STRING"))
        .AddSingleton<IRmqSubscriberClient, DttRmqSubscriberServiceProxy>();
}