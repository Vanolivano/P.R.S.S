FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR app
COPY bin/published .
ENTRYPOINT ["dotnet", "Publication.Rabbit.Subscription.Storage.WebInput.dll"]