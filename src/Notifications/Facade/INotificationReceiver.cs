using System.Threading.Tasks;

namespace Publication.Rabbit.Subscription.Storage.Notifications.Facade;

public interface INotificationReceiver
{
    Task ReceiveAsync(string message);
}
