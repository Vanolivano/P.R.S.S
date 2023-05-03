using Microsoft.Extensions.DependencyInjection;
using Publication.Rabbit.Subscription.Storage.RmqSubscriber.Domain.Services;

namespace Publication.Rabbit.Subscription.Storage.RmqSubscriber.BL.DI
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddRmqSubscriberService(
            this IServiceCollection serviceCollection) =>
            serviceCollection.AddSingleton<IRmqSubscriberService, RmqSubscriberService>();
    }
}