#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base

WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY . .
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
ENTRYPOINT ["dotnet", "ShunHang.Mrt.HttpApi.Host.dll"]
