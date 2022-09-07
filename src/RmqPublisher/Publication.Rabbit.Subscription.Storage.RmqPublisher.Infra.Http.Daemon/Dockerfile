FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR app
COPY bin/published .
ENTRYPOINT ["dotnet", "PRSS.RmqPublisher.Infra.Http.Daemon.dll"]