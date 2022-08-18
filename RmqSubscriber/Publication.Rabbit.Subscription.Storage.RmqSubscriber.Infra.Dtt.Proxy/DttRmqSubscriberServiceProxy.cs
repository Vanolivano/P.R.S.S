using System;
using System.Text;
using Dev.Tools.Configs;
using Dev.Tools.Results;
using Dev.Tools.Results.Builders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Publication.Rabbit.Subscription.Storage.RmqSubscriber.Facade;
using Publication.Rabbit.Subscription.Storage.RmqSubscriber.Facade.Args;
using RabbitMQ.Client;

namespace Publication.Rabbit.Subscription.Storage.RmqSubscriber.Infra.Dtt.Proxy
{
	public class DttRmqSubscriberServiceProxy : IRmqSubscriberClient
	{
		private readonly RabbitConfig _rabbitConfig;
		private readonly ILogger<DttRmqSubscriberServiceProxy> _logger;

		public DttRmqSubscriberServiceProxy(IOptions<RabbitConfig> rabbitConfig,
			ILogger<DttRmqSubscriberServiceProxy> logger)
		{
			_logger = logger;
			_rabbitConfig = rabbitConfig.Value ?? throw new ArgumentNullException(nameof(rabbitConfig));
		}

		public ISuccessData SendData(IPersonArgs personArgs)
		{
			try
			{
				var eventName = personArgs.GetType().Name;
				var factory = new ConnectionFactory
				{
					Uri = new Uri(_rabbitConfig.ConnectionString)
				};
				using var connection = factory.CreateConnection();
				using var channel = connection.CreateModel();
				// channel.ExchangeDeclare(exchange:_rabbitConfig.ExchangeName,
				// 	type: "direct");
				string message = JsonConvert.SerializeObject(personArgs);
				var body = Encoding.UTF8.GetBytes(message);
				channel.BasicPublish(
					exchange: _rabbitConfig.ExchangeName,
					routingKey: eventName,
					basicProperties: null,
					body: body);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);

				return SuccessDataBuilder.BuildError(ex);
			}

			return SuccessDataBuilder.BuildSuccess();
		}
	}
}