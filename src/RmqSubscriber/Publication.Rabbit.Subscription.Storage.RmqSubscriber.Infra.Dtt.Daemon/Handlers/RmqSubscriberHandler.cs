using System;
using System.Text;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Publication.Rabbit.Subscription.Storage.RmqSubscriber.Domain.Services;
using Publication.Rabbit.Subscription.Storage.RmqSubscriber.Infra.Dtt.Daemon.Mappers;
using Publication.Rabbit.Subscription.Storage.RmqSubscriber.Infra.Dtt.Dto;
using RabbitMQ.Client.Events;

namespace Publication.Rabbit.Subscription.Storage.RmqSubscriber.Infra.Dtt.Daemon.Handlers
{
    public class RmqSubscriberHandler : IRmqSubscriberHandler
    {
        private readonly ILogger<RmqSubscriberHandler> _logger;
        private readonly IRmqSubscriberService _rmqSubscriberService;

        public RmqSubscriberHandler(
            ILogger<RmqSubscriberHandler> logger,
            IRmqSubscriberService rmqSubscriberService)
        {
            _logger = logger;
            _rmqSubscriberService = rmqSubscriberService;
        }

        public async void Handle(object model, BasicDeliverEventArgs eventArgs)
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

            try
            {
                await _rmqSubscriberService.SavePersonAsync(personDto.ToModel());
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured while save person. " +
                "Reason {0}", ex.Message);
            }
            _logger.LogInformation("[Person] {0} has been saved to mongodb store.", personDto);

            ((EventingBasicConsumer)model).Model.BasicAck(deliveryTag: eventArgs.DeliveryTag, multiple: false);
            _logger.LogInformation("RabbitMq has been acked.");
        }
    }
}