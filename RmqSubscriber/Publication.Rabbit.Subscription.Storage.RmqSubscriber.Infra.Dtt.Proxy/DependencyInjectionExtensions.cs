using Microsoft.Extensions.DependencyInjection;
using Publication.Rabbit.Subscription.Storage.RmqSubscriber.Facade;

namespace Publication.Rabbit.Subscription.Storage.RmqSubscriber.Infra.Dtt.Proxy
{
	public static class DependencyInjectionExtensions
	{
		public static IServiceCollection AddRmqSubscriberClient(this IServiceCollection serviceCollection) =>
			serviceCollection.AddSingleton<IRmqSubscriberClient, DttRmqSubscriberServiceProxy>();
	}
}