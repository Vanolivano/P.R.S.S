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
using Publication.Rabbit.Subscription.Storage.RmqSubscriber.Infra.Dtt.Proxy.Mappers;
using RabbitMQ.Client;

namespace Publication.Rabbit.Subscription.Storage.RmqSubscriber.Infra.Dtt.Proxy
{
	public class DttRmqSubscriberServiceProxy : IRmqSubscriberClient
	{
		private readonly RabbitConfig _rabbitConfig;
		private readonly ILogger<DttRmqSubscriberServiceProxy> _logger;

		public DttRmqSubscriberServiceProxy(
			IOptions<RabbitConfig> rabbitConfig,
			ILogger<DttRmqSubscriberServiceProxy> logger)
		{
			_logger = logger;
			_rabbitConfig = rabbitConfig.Value ?? throw new ArgumentNullException(nameof(rabbitConfig));
		}

		public ISuccessData SendData(IPersonArgs personArgs)
		{
			try
			{
				var dto = personArgs.ToDto();
				var factory = new ConnectionFactory
				{
					Uri = new Uri(_rabbitConfig.ConnectionString)
				};
				//TODO: Подумать, создавать канал каждый раз при отправке сообщения или создать один канал на время жизни сервиса
				using var connection = factory.CreateConnection();
				using var channel = connection.CreateModel();

				string message = JsonConvert.SerializeObject(dto);
				var body = Encoding.UTF8.GetBytes(message);
				channel.BasicPublish(
					exchange: string.Empty,
					routingKey: dto.PostKey,
					basicProperties: null,
					body: body);

				_logger.LogInformation($"Data: {dto} successfully sent to rabbit.");
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