using System;
using System.Text;
using Dev.Tools.Configs;
using Dev.Tools.Results;
using Dev.Tools.Results.Builders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Publication.Rabbit.Subscription.Storage.Notifications.Domain;
using Publication.Rabbit.Subscription.Storage.Notifications.Infra.Dto;
using RabbitMQ.Client;

namespace Publication.Rabbit.Subscription.Storage.RmqSubscriber.Infra.Dtt.Proxy
{
	public class NotificationPusher : INotificationPusher
	{
		private readonly RabbitConfig _rabbitConfig;
		private readonly ILogger<NotificationPusher> _logger;

		public NotificationPusher(
			IOptions<RabbitConfig> rabbitConfig,
			ILogger<NotificationPusher> logger)
		{
			_logger = logger;
			_rabbitConfig = rabbitConfig.Value ?? throw new ArgumentNullException(nameof(rabbitConfig));
		}

		public ISuccessData Push(string message)
		{
			try
			{
				var factory = new ConnectionFactory
				{
					Uri = new Uri(_rabbitConfig.ConnectionString)
				};
				//TODO: Подумать, создавать канал каждый раз при отправке сообщения или создать один канал на время жизни сервиса
				using var connection = factory.CreateConnection();
				using var channel = connection.CreateModel();

				//string data = JsonConvert.SerializeObject(message);
				var body = Encoding.UTF8.GetBytes(message);
				channel.BasicPublish(
					exchange: string.Empty,
					routingKey: NotificationsConstants.PostKey,
					basicProperties: null,
					body: body);

				_logger.LogInformation($"Data: {message} successfully sent to rabbit.");
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