FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 5011

ENV ASPNETCORE_URLS=http://+:5011

RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ESC_MANEJO.WEB/ESC_MANEJO.WEB.csproj", "ESC_MANEJO.WEB/"]
RUN dotnet restore "ESC_MANEJO.WEB/ESC_MANEJO.WEB.csproj"
COPY . .
WORKDIR "/src/ESC_MANEJO.WEB"
RUN dotnet build "ESC_MANEJO.WEB.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ESC_MANEJO.WEB.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ESC_MANEJO.WEB.dll"]
