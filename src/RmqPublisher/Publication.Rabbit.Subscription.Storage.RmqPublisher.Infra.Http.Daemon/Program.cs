using Microsoft.AspNetCore.Builder;
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
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddValidation();
			builder.Services.AddBearerAuthorization(builder.Configuration);
			builder.Services.AddRmqPublisherService();
			builder.Services.AddRmqSubscriberClient(builder.Configuration);
			var app = builder.Build();

			app.MapControllers();

			app.Run();
		}
	}
}