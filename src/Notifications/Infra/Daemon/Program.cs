using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Dev.Tools.Configs;
using Microsoft.Extensions.Configuration;
using Publication.Rabbit.Subscription.Storage.Notifications.Infra.Proxy.DI;
using Publication.Rabbit.Subscription.Storage.Notifications.BL.DI;

namespace Publication.Rabbit.Subscription.Storage.RmqPublisher.Infra.Http.Daemon
{
    internal class Program
	{
		internal static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddControllers();
			builder.Services.AddHttpClient();
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddNotificationPusher();
			builder.Services.AddNotificationService();
			// builder.Services.AddValidation();
			// builder.Services.AddBearerAuthorization();
			builder.Services.Configure<RabbitConfig>(config =>
				config.ConnectionString = builder.Configuration.GetValue<string>("RABBIT_MQ_CONNECTION_STRING"));
			// builder.Services.Configure<AuthConfig>(config =>
				// config.AuthToken = builder.Configuration.GetValue<string>("AUTH_TOKEN"));

			var app = builder.Build();

			app.MapControllers();

			app.Run();
		}
	}
}