using Microsoft.Extensions.DependencyInjection;
using Publication.Rabbit.Subscription.Storage.RmqPublisher.Infra.Http.Daemon.Authorizers.Bearer;

namespace Publication.Rabbit.Subscription.Storage.RmqPublisher.Infra.Http.Daemon.Authorizers
{
	public static class DependencyInjector
	{
		public static IServiceCollection AddBearerAuthorization(this IServiceCollection serviceCollection) =>
			serviceCollection.AddSingleton<IBearerAuthorizer, BearerAuthorizer>();
	}
}