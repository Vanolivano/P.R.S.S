using Publication.Rabbit.Subscription.Storage.Notifications.Domain;
using Dev.Tools.Results;
using System.Threading;
using System.Threading.Tasks;

namespace Publication.Rabbit.Subscription.Storage.Notifications.BL;

public class NotificationService : INotificationService
{
    private readonly INotificationPusher _notificationPusher;

    public NotificationService(INotificationPusher notificationPusher)
    {
        _notificationPusher = notificationPusher;
    }

    public Task<ISuccessData> HandleAsync(string message, CancellationToken token)
    {
        var result = _notificationPusher.Push(message);
        return Task.FromResult(result);
    }
}