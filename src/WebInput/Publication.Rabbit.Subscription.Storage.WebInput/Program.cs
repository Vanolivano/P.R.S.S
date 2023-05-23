using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Publication.Rabbit.Subscription.Storage.RmqPublisher.Infra.Http.Proxy.DI;
using Publication.Rabbit.Subscription.Storage.Notifications.Infra.Proxy.DI;

namespace Publication.Rabbit.Subscription.Storage.WebInput;
class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddRazorPages();
        builder.Services.AddSignalR();
        builder.Services.AddServerSideBlazor();

        builder.Services.AddRmqPublisherClient(builder.Configuration);
        builder.Services.AddNotificationClient(builder.Configuration);
        builder.Services.AddNotificationSubscriber<NotificationReceiver>(builder.Configuration);

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
        }

        app.UseStaticFiles();
        app.UseRouting();
        app.MapBlazorHub();
        app.MapHub<NotificationHub>(Constants.NotificationHubRelativeUri);
        app.MapFallbackToPage("/_Host");

        app.Run();
    }
}