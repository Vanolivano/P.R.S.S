using Dev.Tools.Configs;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Publication.Rabbit.Subscription.Storage.RmqPublisher.BL.Services;
using Publication.Rabbit.Subscription.Storage.RmqPublisher.Infra.Http.Daemon.Authorizers;
using Publication.Rabbit.Subscription.Storage.RmqPublisher.Infra.Http.Daemon.Validators;
using Publication.Rabbit.Subscription.Storage.RmqSubscriber.Infra.Dtt.Proxy.DI;

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
			builder.Services.AddValidation();
			builder.Services.AddBearerAuthorization();
			builder.Services.AddRmqPublisherService();
			builder.Services.AddRmqSubscriberClient();
			builder.Services.Configure<RabbitConfig>(config =>
				config.ConnectionString = builder.Configuration.GetValue<string>("RABBIT_MQ_CONNECTION_STRING"));
			builder.Services.Configure<AuthConfig>(config =>
				config.AuthToken = builder.Configuration.GetValue<string>("AUTH_TOKEN"));

			var app = builder.Build();

			app.UseHttpsRedirection();

			app.MapControllers();

			app.Run();
		}
	}
}