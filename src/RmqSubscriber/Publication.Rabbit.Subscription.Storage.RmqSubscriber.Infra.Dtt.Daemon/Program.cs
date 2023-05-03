using Dev.Tools.Configs;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Publication.Rabbit.Subscription.Storage.RmqSubscriber.BL.DI;
using Publication.Rabbit.Subscription.Storage.RmqSubscriber.Infra.Db.DI;
using Publication.Rabbit.Subscription.Storage.RmqSubscriber.Infra.Dtt.Daemon.DI;

namespace Publication.Rabbit.Subscription.Storage.RmqSubscriber.Infra.Dtt.Daemon
{
    internal static class Program
	{
		internal static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.Configure<RabbitConfig>(config =>
			{
				config.ConnectionString = builder.Configuration.GetValue<string>("RABBIT_MQ_CONNECTION_STRING");
			});
			builder.Services.AddRmqSubscriberHandler();
			builder.Services.AddRmqSubscriberService();
			builder.Services.AddPersonMongoRepository();
			builder.Services.AddHostedService<SubscribeHostedService>();

			var app = builder.Build();

			app.Run();
		}
	}
}