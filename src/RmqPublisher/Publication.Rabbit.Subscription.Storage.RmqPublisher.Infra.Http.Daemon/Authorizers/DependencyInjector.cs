using Dev.Tools.Configs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Publication.Rabbit.Subscription.Storage.RmqPublisher.Infra.Http.Daemon.Authorizers.Bearer;

namespace Publication.Rabbit.Subscription.Storage.RmqPublisher.Infra.Http.Daemon.Authorizers;

public static class DependencyInjector
{
    public static IServiceCollection AddBearerAuthorization(this IServiceCollection services, IConfiguration conf) =>
        services
            .Configure<AuthConfig>(ac => ac.AuthToken = conf.GetValue<string>("AUTH_TOKEN"))
            .AddSingleton<IBearerAuthorizer, BearerAuthorizer>();
}