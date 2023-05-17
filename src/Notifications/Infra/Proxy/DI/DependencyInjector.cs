using Microsoft.Extensions.DependencyInjection;
using Publication.Rabbit.Subscription.Storage.Notifications.Facade;
using Publication.Rabbit.Subscription.Storage.Notifications.Infra.Proxy.Handlers;

namespace Publication.Rabbit.Subscription.Storage.Notifications.Infra.Proxy.DI;

public static class DependencyInjector
{
    public static IServiceCollection AddNotificationsService<T>(this IServiceCollection serviceCollection)
		where T : class, INotificationReceiver => serviceCollection
			.AddSingleton<INotificationReceiver, T>()
			.AddSingleton<INotificationsHandler, NotificationsHandler>()
			.AddHostedService<NotificationsHostedService>();

	public static IServiceCollection AddNotificationSender(this IServiceCollection serviceCollection) =>
		serviceCollection.AddSingleton<INotificationService, HttpNotificationServiceProxy>();
}
