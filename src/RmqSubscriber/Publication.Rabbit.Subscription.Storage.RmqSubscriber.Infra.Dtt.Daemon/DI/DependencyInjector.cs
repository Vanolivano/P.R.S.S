using Dev.Tools.Configs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Publication.Rabbit.Subscription.Storage.RmqSubscriber.Infra.Dtt.Daemon.Handlers;

namespace Publication.Rabbit.Subscription.Storage.RmqSubscriber.Infra.Dtt.Daemon.DI;
public static class DependencyInjector
{
    public static void AddRmqSubscriberHandler(this IServiceCollection services, IConfiguration conf)
    {
        services
			.Configure<RabbitConfig>(config =>
			{
				config.ConnectionString = conf.GetValue<string>("RABBIT_MQ_CONNECTION_STRING");
			})
			.AddSingleton<IRmqSubscriberHandler, RmqSubscriberHandler>()
        	.AddHostedService<SubscribeHostedService>();
    }
}