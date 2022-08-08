FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Publication.Rabbit.Subscription.Storage/Publication.Rabbit.Subscription.Storage.csproj", "Publication.Rabbit.Subscription.Storage/"]
RUN dotnet restore "Publication.Rabbit.Subscription.Storage/Publication.Rabbit.Subscription.Storage.csproj"
COPY . .
WORKDIR "/src/Publication.Rabbit.Subscription.Storage"
RUN dotnet build "Publication.Rabbit.Subscription.Storage.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Publication.Rabbit.Subscription.Storage.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Publication.Rabbit.Subscription.Storage.dll"]
