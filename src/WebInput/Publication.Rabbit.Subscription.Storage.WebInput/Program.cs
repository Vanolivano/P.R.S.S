using System;
using Dev.Tools.Configs;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Publication.Rabbit.Subscription.Storage.RmqPublisher.Infra.Http.Proxy.DI;
using Publication.Rabbit.Subscription.Storage.Notifications.Infra.Proxy.DI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddRmqPublisherClient();
builder.Services.AddNotificationSender();
builder.Services.AddNotificationHttpClient(builder.Configuration);

var httpClientName =
    builder.Configuration.GetValue<string>("RMQ_PUBLISHER_HTTP_CLIENT_NAME_STRING");
var httpClientBaseAddress =
    builder.Configuration.GetValue<string>("RMQ_PUBLISHER_HTTP_CLIENT_BASE_ADDRESS");

builder.Services.AddHttpClient(httpClientName, x => { x.BaseAddress = new Uri(httpClientBaseAddress); });

builder.Services.Configure<HttpClientConfig>(config =>
{
    config.HttpClientName = httpClientName;
    config.HttpClientBaseAddress = httpClientBaseAddress;
});
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

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();