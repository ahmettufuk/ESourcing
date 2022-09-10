#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ESourcing.Order/ESourcing.Order.csproj", "ESourcing.Order/"]
COPY ["ESourcing.Application/ESourcing.Application.csproj", "ESourcing.Application/"]
COPY ["Ordering.Domain/Ordering.Domain.csproj", "Ordering.Domain/"]
COPY ["ESourcing.Infrastracture/ESourcing.Infrastracture.csproj", "ESourcing.Infrastracture/"]
COPY ["EventBusRabbitMQ/EventBusRabbitMQ.csproj", "EventBusRabbitMQ/"]
RUN dotnet restore "ESourcing.Order/ESourcing.Order.csproj"
COPY . .
WORKDIR "/src/ESourcing.Order"
RUN dotnet build "ESourcing.Order.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ESourcing.Order.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ESourcing.Order.dll"]