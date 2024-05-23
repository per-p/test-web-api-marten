FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine3.19  AS base
RUN apk add --upgrade --no-cache icu-libs=74.1-r0
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
WORKDIR "/app"
EXPOSE 8080


FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine3.19 AS build
WORKDIR "/src"

COPY . ./
WORKDIR "./WebApi"
RUN dotnet build "./WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./WebApi.csproj" -c Release --no-restore -o /app/publish

FROM base AS final
HEALTHCHECK --interval=5m --timeout=3s CMD wget --no-verbose --tries=2 --output-document=/dev/null http://0.0.0.0:8080/ &>/dev/null || exit 1
USER app
COPY --chown=app:app --chmod=555 --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApi.dll"]
