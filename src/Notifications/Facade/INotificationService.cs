using System.Threading;
using System.Threading.Tasks;

namespace Publication.Rabbit.Subscription.Storage.Notifications.Facade;

public interface INotificationService
{
    Task PushMessageAsync(string message, CancellationToken cancellationToken);
}