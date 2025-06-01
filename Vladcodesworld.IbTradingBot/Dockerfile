FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and projects
COPY ./Vladcodesworld.IbTradingBot.sln ./
COPY ./IbTradingBot.Console/IbTradingBot.Console.csproj ./IbTradingBot.Console/
COPY ./IbTradingBot.ProxyClient/IbTradingBot.ProxyClient.csproj ./IbTradingBot.ProxyClient/

# Restore
RUN dotnet restore Vladcodesworld.IbTradingBot.sln

# Copy the rest of the files
COPY . .

# Build
WORKDIR /src/IbTradingBot.Console
RUN dotnet publish -c Release -o /app/publish

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "IbTradingBot.Console.dll"]
