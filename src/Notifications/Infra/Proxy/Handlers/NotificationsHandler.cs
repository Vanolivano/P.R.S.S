using System;
using System.Text;
using Microsoft.Extensions.Logging;
using Publication.Rabbit.Subscription.Storage.Notifications.Facade;
using RabbitMQ.Client.Events;

namespace Publication.Rabbit.Subscription.Storage.Notifications.Infra.Proxy.Handlers
{
    public class NotificationsHandler : INotificationsHandler
    {
        private readonly ILogger<NotificationsHandler> _logger;
        private readonly INotificationReceiver _clientService;

        public NotificationsHandler(
            ILogger<NotificationsHandler> logger,
            INotificationReceiver clientService)
        {
            _logger = logger;
            _clientService = clientService ?? throw new ArgumentNullException(nameof(clientService));
        }

        public async void Handle(object model, BasicDeliverEventArgs eventArgs)
        {
            var body = eventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            if (string.IsNullOrWhiteSpace(message))
            {
                _logger.LogWarning("Message cannot be null or empty");
                return;
            }

            _logger.LogInformation("[message] {0}", message);

            try
            {
                await _clientService.ReceiveAsync(message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured while recieve message. " +
                "Reason {0}", ex.Message);
            }
            _logger.LogInformation("[message] {0} has been processed.", message);

            ((EventingBasicConsumer)model).Model.BasicAck(deliveryTag: eventArgs.DeliveryTag, multiple: false);
            _logger.LogDebug("RabbitMq has been acked.");
        }
    }
}