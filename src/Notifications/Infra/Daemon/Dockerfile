FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR app
COPY bin/published .
ENTRYPOINT ["dotnet", "P.R.S.S.Notifications.Infra.Daemon.dll"]