using Microsoft.Extensions.DependencyInjection;
using Publication.Rabbit.Subscription.Storage.RmqPublisher.Facade;

namespace Publication.Rabbit.Subscription.Storage.RmqPublisher.Infra.Http.Proxy
{
	public static class DependencyInjectionExtensions
	{
		public static IServiceCollection AddRmqPublisherClient(this IServiceCollection serviceCollection) =>
			serviceCollection.AddSingleton<IRmqPublisherClient, HttpRmqPublisherServiceProxy>();
	}
}