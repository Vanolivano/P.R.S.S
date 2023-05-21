using System.Threading;
using System.Threading.Tasks;
using Dev.Tools.Results;

namespace Publication.Rabbit.Subscription.Storage.Notifications.Facade;

public interface INotificationClient
{
    Task<ISuccessData> PushMessageAsync(string message, CancellationToken cancellationToken = default);
}