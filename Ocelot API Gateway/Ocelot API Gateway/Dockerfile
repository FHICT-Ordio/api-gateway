#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 1000
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Ocelot API Gateway/Ocelot API Gateway.csproj", "Ocelot API Gateway/"]
RUN dotnet restore "Ocelot API Gateway/Ocelot API Gateway.csproj"
COPY . .
WORKDIR "/src/Ocelot API Gateway"
RUN dotnet build "Ocelot API Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ocelot API Gateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ocelot API Gateway.dll"]