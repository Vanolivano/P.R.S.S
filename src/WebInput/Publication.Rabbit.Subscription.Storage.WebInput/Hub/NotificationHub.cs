using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Publication.Rabbit.Subscription.Storage.WebInput;

public class NotificationHub : Hub
{
    public async Task SendMessage(string message)
    {
        await Clients.All.SendAsync(Constants.ReceiveMessage, message);
    }
}