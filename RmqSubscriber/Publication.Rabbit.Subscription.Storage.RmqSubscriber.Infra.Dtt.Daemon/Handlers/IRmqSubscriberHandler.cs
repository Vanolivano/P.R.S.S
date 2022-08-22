using RabbitMQ.Client.Events;

namespace Publication.Rabbit.Subscription.Storage.RmqSubscriber.Infra.Dtt.Daemon.Handlers
{
	public interface IRmqSubscriberHandler
	{
		void Handle(object model, BasicDeliverEventArgs eventArgs);
	}
}