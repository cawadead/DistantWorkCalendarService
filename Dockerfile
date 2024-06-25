FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app/

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src/
USER root
COPY --chown=2001:0 /src/ .
WORKDIR /src/DistantWorkCalendarService
RUN dotnet build DistantWorkCalendarService.csproj -c Release -o /app/publish

FROM build AS publish
RUN dotnet publish DistantWorkCalendarService.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app/
COPY --chown=2001:0 --from=publish /app/publish /app
ENTRYPOINT ["dotnet", "DistantWorkCalendarService.dll"]
