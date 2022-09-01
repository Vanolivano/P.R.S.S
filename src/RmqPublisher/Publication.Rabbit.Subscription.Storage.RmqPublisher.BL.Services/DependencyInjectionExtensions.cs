using Microsoft.Extensions.DependencyInjection;
using Publication.Rabbit.Subscription.Storage.RmqPublisher.Domain.Services;

namespace Publication.Rabbit.Subscription.Storage.RmqPublisher.BL.Services
{
	public static class DependencyInjectionExtensions
	{
		public static IServiceCollection AddRmqPublisherService(this IServiceCollection serviceCollection) =>
			serviceCollection.AddSingleton<IRmqPublisherService, RmqPublisherService>();
	}
}