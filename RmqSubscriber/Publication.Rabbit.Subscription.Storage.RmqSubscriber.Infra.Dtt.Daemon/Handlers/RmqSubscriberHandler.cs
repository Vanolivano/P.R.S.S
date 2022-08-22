using System.Text;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Publication.Rabbit.Subscription.Storage.RmqSubscriber.Infra.Dtt.Dto;
using RabbitMQ.Client.Events;

namespace Publication.Rabbit.Subscription.Storage.RmqSubscriber.Infra.Dtt.Daemon.Handlers
{
	public class RmqSubscriberHandler : IRmqSubscriberHandler
	{
		private readonly ILogger<RmqSubscriberHandler> _logger;

		public RmqSubscriberHandler(ILogger<RmqSubscriberHandler> logger)
		{
			_logger = logger;
		}

		public void Handle(object model, BasicDeliverEventArgs eventArgs)
		{
			var body = eventArgs.Body.ToArray();
			var message = Encoding.UTF8.GetString(body);

			var personDto = JsonConvert.DeserializeObject<PersonDto>(message);

			if (personDto == null)
			{
				_logger.LogWarning("It wasn't possible to deserialize message into PersonDto. Message: {0}", message);
				return;
			}

			_logger.LogInformation("[Person] {0}", personDto);
			((EventingBasicConsumer) model).Model.BasicAck(deliveryTag: eventArgs.DeliveryTag, multiple: false);
		}
	}
}