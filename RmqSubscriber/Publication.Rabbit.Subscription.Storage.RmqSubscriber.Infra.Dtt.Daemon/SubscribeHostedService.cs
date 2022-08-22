using System;
using System.Threading;
using System.Threading.Tasks;
using Dev.Tools.Configs;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Publication.Rabbit.Subscription.Storage.RmqSubscriber.Infra.Dtt.Daemon.Handlers;
using Publication.Rabbit.Subscription.Storage.RmqSubscriber.Infra.Dtt.Dto;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Publication.Rabbit.Subscription.Storage.RmqSubscriber.Infra.Dtt.Daemon
{
	public class SubscribeHostedService : IHostedService
	{
		private readonly IModel _channel;
		private readonly IConnection _connection;
		private readonly ILogger<SubscribeHostedService> _logger;
		private readonly IRmqSubscriberHandler _rmqSubscriberHandler;

		public SubscribeHostedService(
			IOptions<RabbitConfig> rabbitConfig,
			ILogger<SubscribeHostedService> logger ,
			IRmqSubscriberHandler rmqSubscriberHandler)
		{
			_logger = logger;
			_rmqSubscriberHandler = rmqSubscriberHandler;
			var factory = new ConnectionFactory
			{
				Uri = new Uri(rabbitConfig.Value.ConnectionString)
			};

			_connection = factory.CreateConnection();
			_channel = _connection.CreateModel();
		}

		public Task StartAsync(CancellationToken cancellationToken)
		{
			try
			{
				Subscribe();
			}
			catch (Exception exception)
			{
				_logger.LogError($"The error occurred during the subscription. Reason: {exception.Message}");
			}

			return Task.CompletedTask;
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			_channel.Dispose();
			_connection.Dispose();
			return Task.CompletedTask;
		}

		private void Subscribe()
		{
			_channel.QueueDeclare(
				queue: DtoConstants.PostKey,
				durable: true,
				exclusive: false,
				autoDelete: false);

			var consumer = new EventingBasicConsumer(_channel);
			consumer.Received += _rmqSubscriberHandler.Handle;
			_channel.BasicConsume(
				queue: DtoConstants.PostKey,
				autoAck: false,
				consumer: consumer);
		}
	}
}