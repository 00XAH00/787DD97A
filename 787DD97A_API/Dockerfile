#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["787DD97A_API/787DD97A_API.csproj", "787DD97A_API/"]
COPY ["787DD97A/CalcForPriceFlat.csproj", "787DD97A/"]
RUN dotnet restore "787DD97A_API/787DD97A_API.csproj"
COPY . .
WORKDIR "/src/787DD97A_API"
RUN dotnet build "787DD97A_API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "787DD97A_API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "787DD97A_API.dll"]
