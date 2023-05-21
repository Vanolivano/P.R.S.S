using Dev.Tools.Results;

namespace Publication.Rabbit.Subscription.Storage.Notifications.Domain;

public interface INotificationPusher
{
    ISuccessData Push(string message);
}