using Microsoft.Extensions.DependencyInjection;
using Publication.Rabbit.Subscription.Storage.RmqSubscriber.Domain.Repositories;
using Publication.Rabbit.Subscription.Storage.RmqSubscriber.Infra.Db;

namespace Publication.Rabbit.Subscription.Storage.RmqSubscriber.Infra.Db.DI
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddPersonMongoRepository(
            this IServiceCollection serviceCollection) =>
            serviceCollection.AddSingleton<IPersonRepository, PersonRepository>();
    }
}