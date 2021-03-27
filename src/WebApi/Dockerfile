FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
WORKDIR /app
COPY ./bin/Release/net5.0/publish/ ./
ENTRYPOINT ["dotnet", "WebApi.dll"]