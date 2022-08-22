using Dev.Tools.Configs;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Publication.Rabbit.Subscription.Storage.RmqPublisher.BL.Services;
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
			builder.Services.AddSwaggerGen();
			builder.Services.AddValidation();
			builder.Services.AddRmqPublisherService();
			builder.Services.AddRmqSubscriberClient();
			builder.Services.Configure<RabbitConfig>(config =>
			{
				config.ExchangeName = builder.Configuration.GetValue<string>("RABBIT_MQ_EXCHANGE_NAME_STRING");
				config.ConnectionString = builder.Configuration.GetValue<string>("RABBIT_MQ_CONNECTION_STRING");
			});


			var app = builder.Build();

			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();

			app.MapControllers();

			app.Run();
		}
	}
}