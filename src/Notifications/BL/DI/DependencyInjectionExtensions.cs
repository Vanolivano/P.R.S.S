using Microsoft.Extensions.DependencyInjection;
using Publication.Rabbit.Subscription.Storage.Notifications.Domain;

namespace Publication.Rabbit.Subscription.Storage.Notifications.BL.DI
{
	public static class DependencyInjectionExtensions
	{
		public static IServiceCollection AddNotificationService(this IServiceCollection serviceCollection) =>
			serviceCollection.AddSingleton<INotificationService, NotificationService>();
	}
}