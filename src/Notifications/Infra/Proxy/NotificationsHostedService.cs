using System;
using System.Threading;
using System.Threading.Tasks;
using Dev.Tools.Configs;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using Publication.Rabbit.Subscription.Storage.Notifications.Infra.Dto;
using Publication.Rabbit.Subscription.Storage.Notifications.Infra.Proxy.Handlers;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Publication.Rabbit.Subscription.Storage.Notifications.Infra.Proxy
{
    public class NotificationsHostedService : IHostedService
    {
        private IModel _channel;
        private IConnection _connection;
        private const int RetryCount = 10;
        private readonly ConnectionFactory _connectionFactory;
        private readonly ILogger<NotificationsHostedService> _logger;
        private readonly INotificationsHandler _rmqSubscriberHandler;

        public NotificationsHostedService(
            IOptions<RabbitConfig> rabbitConfig,
            ILogger<NotificationsHostedService> logger,
            INotificationsHandler rmqSubscriberHandler)
        {
            _logger = logger;
            _rmqSubscriberHandler = rmqSubscriberHandler;
            _connectionFactory = new ConnectionFactory
            {
                Uri = new Uri(rabbitConfig.Value.ConnectionString)
            };
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var policy = Policy.Handle<Exception>().WaitAndRetry(
                RetryCount,
                retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                OnRetry);

            policy.Execute(Handle);
            return Task.CompletedTask;


            void OnRetry(Exception ex, TimeSpan time)
            {
                _logger.LogWarning("Could not subscribe in {0}. Reason: {1}", time, ex.Message);
            }
        }

        private void Handle()
        {
            _connection = _connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();
            Subscribe();
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
                queue: NotificationsConstants.PostKey,
                durable: true,
                exclusive: false,
                autoDelete: false);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += _rmqSubscriberHandler.Handle;
            _channel.BasicConsume(
                queue: NotificationsConstants.PostKey,
                autoAck: false,
                consumer: consumer);
        }
    }
}