using System;
using Dev.Tools.Configs;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Publication.Rabbit.Subscription.Storage.RmqPublisher.Infra.Http.Proxy.DI;
using Publication.Rabbit.Subscription.Storage.Notifications.Infra.Proxy.DI;
using Publication.Rabbit.Subscription.Storage.WebInput;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSignalR();
builder.Services.AddServerSideBlazor();

builder.Services.AddRmqPublisherClient(builder.Configuration);
builder.Services.AddNotificationClient();
builder.Services.AddNotificationHttpClient(builder.Configuration);
builder.Services.AddNotificationSubscriber<NotificationReceiver>();

builder.Services.Configure<RabbitConfig>(config =>
    config.ConnectionString = builder.Configuration.GetValue<string>("RABBIT_MQ_CONNECTION_STRING"));
builder.Services.Configure<AuthConfig>(config =>
{
    config.AuthToken = builder.Configuration.GetValue<string>("AUTH_TOKEN");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapHub<NotificationHub>("/notificationHub");
app.MapFallbackToPage("/_Host");

app.Run();