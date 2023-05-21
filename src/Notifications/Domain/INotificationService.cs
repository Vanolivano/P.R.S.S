using System.Threading;
using System.Threading.Tasks;
using Dev.Tools.Results;

namespace Publication.Rabbit.Subscription.Storage.Notifications.Domain;

public interface INotificationService
{
    Task<ISuccessData> HandleAsync(string message, CancellationToken token = default);
}