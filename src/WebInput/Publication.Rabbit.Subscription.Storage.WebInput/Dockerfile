FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/WebInput/Publication.Rabbit.Subscription.Storage.WebInput/Publication.Rabbit.Subscription.Storage.WebInput.csproj", "Publication.Rabbit.Subscription.Storage.WebInput/"]
RUN dotnet restore "src/WebInput/Publication.Rabbit.Subscription.Storage.WebInput/Publication.Rabbit.Subscription.Storage.WebInput.csproj"
COPY . .
WORKDIR "/src/Publication.Rabbit.Subscription.Storage.WebInput"
RUN dotnet build "Publication.Rabbit.Subscription.Storage.WebInput.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Publication.Rabbit.Subscription.Storage.WebInput.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Publication.Rabbit.Subscription.Storage.WebInput.dll"]
