FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["RmqPublisher/Publication.Rabbit.Subscription.Storage.RmqPublisher.Infra.Http.Daemon/Publication.Rabbit.Subscription.Storage.RmqPublisher.Infra.Http.Daemon.csproj", "Publication.Rabbit.Subscription.Storage.RmqPublisher.Infra.Http.Daemon/"]
RUN dotnet restore "RmqPublisher/Publication.Rabbit.Subscription.Storage.RmqPublisher.Infra.Http.Daemon/Publication.Rabbit.Subscription.Storage.RmqPublisher.Infra.Http.Daemon.csproj"
COPY . .
WORKDIR "/src/Publication.Rabbit.Subscription.Storage.RmqPublisher.Infra.Http.Daemon"
RUN dotnet build "Publication.Rabbit.Subscription.Storage.RmqPublisher.Infra.Http.Daemon.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Publication.Rabbit.Subscription.Storage.RmqPublisher.Infra.Http.Daemon.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Publication.Rabbit.Subscription.Storage.RmqPublisher.Infra.Http.Daemon.dll"]
