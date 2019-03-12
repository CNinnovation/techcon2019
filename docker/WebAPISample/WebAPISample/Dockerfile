FROM microsoft/dotnet:2.2-aspnetcore-runtime-stretch-slim AS base
WORKDIR /app
HEALTHCHECK --interval=5s --timeout=10s --retries=3 CMD curl --fail http://0.0.0.0/healthz || exit 1
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk-stretch AS build
WORKDIR /src
COPY ["WebAPISample/WebAPISample.csproj", "WebAPISample/"]
RUN dotnet restore "WebAPISample/WebAPISample.csproj"
COPY . .
WORKDIR "/src/WebAPISample"
RUN dotnet build "WebAPISample.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "WebAPISample.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WebAPISample.dll"]