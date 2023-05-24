using Microsoft.AspNetCore.Builder;
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
			
			builder.Services.AddRmqSubscriberHandler(builder.Configuration);
			builder.Services.AddRmqSubscriberService();
			builder.Services.AddPersonMongoRepository(builder.Configuration);

			var app = builder.Build();

			app.Run();
		}
	}
}