using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Publication.Rabbit.Subscription.Storage.Notifications.Facade;

namespace Publication.Rabbit.Subscription.Storage.WebInput;

public class NotificationReceiver : INotificationReceiver
{
    private readonly IHubContext<NotificationHub> _notificationHubContext;
    private readonly ILogger<NotificationReceiver> _logger;

    public NotificationReceiver(
        IHubContext<NotificationHub> notificationHubContext,
         ILogger<NotificationReceiver> logger)
    {
        _notificationHubContext = notificationHubContext ?? throw new System.ArgumentNullException(nameof(notificationHubContext));
        _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
    }

    public async Task ReceiveAsync(string message)
    {
        _logger.LogInformation("Received message: {0}", message);
        await _notificationHubContext.Clients.All.SendAsync("ReceiveMessage", message);
        _logger.LogInformation("Message: {0} has been sended all clients.", message);
    }
}