using RabbitMQ.Client.Events;

namespace Publication.Rabbit.Subscription.Storage.Notifications.Infra.Proxy.Handlers
{
	public interface INotificationsHandler
	{
		void Handle(object model, BasicDeliverEventArgs eventArgs);
	}
}