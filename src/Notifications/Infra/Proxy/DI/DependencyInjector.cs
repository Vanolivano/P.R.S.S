using System;
using Dev.Tools.Configs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Publication.Rabbit.Subscription.Storage.Notifications.Domain;
using Publication.Rabbit.Subscription.Storage.Notifications.Facade;
using Publication.Rabbit.Subscription.Storage.Notifications.Infra.Proxy.Handlers;
using Publication.Rabbit.Subscription.Storage.RmqSubscriber.Infra.Dtt.Proxy;

namespace Publication.Rabbit.Subscription.Storage.Notifications.Infra.Proxy.DI;

public static class DependencyInjector
{
    public static IServiceCollection AddNotificationSubscriber<T>(this IServiceCollection serviceCollection, IConfiguration conf)
        where T : class, INotificationReceiver => serviceCollection
            .Configure<RabbitConfig>(rc => rc.ConnectionString = conf.GetValue<string>("RABBIT_MQ_CONNECTION_STRING"))
            .AddSingleton<INotificationReceiver, T>()
            .AddSingleton<INotificationsHandler, NotificationsHandler>()
            .AddHostedService<NotificationsHostedService>();

    public static void AddNotificationClient(this IServiceCollection services, IConfiguration conf)
    {
        services
            .AddSingleton<INotificationClient, HttpNotificationClientProxy>()
            .AddHttpClient(
                Constants.NotificationsHttpClientName,
                httpClient =>
                {
                    httpClient.BaseAddress = new Uri(conf.GetValue<string>("NOTIFICATION_HTTP_CLIENT_BASE_ADDRESS"));
                });
    }

    public static IServiceCollection AddNotificationPusher(this IServiceCollection serviceCollection) =>
        serviceCollection.AddSingleton<INotificationPusher, NotificationPusher>();
}
