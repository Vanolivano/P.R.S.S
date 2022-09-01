using Microsoft.Extensions.DependencyInjection;
using Publication.Rabbit.Subscription.Storage.RmqSubscriber.Facade;
using Publication.Rabbit.Subscription.Storage.RmqSubscriber.Infra.Dtt.Daemon.Handlers;

namespace Publication.Rabbit.Subscription.Storage.RmqSubscriber.Infra.Dtt.Daemon.DI
{
	public static class DependencyInjector
	{
		public static IServiceCollection AddRmqSubscriberHandler(this IServiceCollection serviceCollection) =>
			serviceCollection.AddSingleton<IRmqSubscriberHandler, RmqSubscriberHandler>();
	}
}