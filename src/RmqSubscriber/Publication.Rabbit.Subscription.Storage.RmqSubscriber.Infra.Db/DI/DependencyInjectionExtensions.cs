using Dev.Tools.Configs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Publication.Rabbit.Subscription.Storage.RmqSubscriber.Domain.Repositories;

namespace Publication.Rabbit.Subscription.Storage.RmqSubscriber.Infra.Db.DI;

public static class DependencyInjectionExtensions
{
    public static void AddPersonMongoRepository(
        this IServiceCollection services, IConfiguration conf)
    {
        services
            .AddSingleton<IPersonRepository, PersonRepository>()
            .Configure<MongoDbConfig>(config =>
            {
                config.ConnectionString = conf.GetValue<string>("MONGODB_DB_CONNECTION_STRING");
            });
    }
}