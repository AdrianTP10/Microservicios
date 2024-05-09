#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["UserMicroservice.Api/UserMicroservice.Api.csproj", "UserMicroservice.Api/"]
COPY ["UserMicroservice.ApplicationServices/UserMicroservice.ApplicationServices.csproj", "UserMicroservice.ApplicationServices/"]
COPY ["UserMicroservice.Core/UserMicroservice.Core.csproj", "UserMicroservice.Core/"]
COPY ["UserMicroservice.DataAccess/UserMicroservice.DataAccess.csproj", "UserMicroservice.DataAccess/"]
RUN dotnet restore "UserMicroservice.Api/UserMicroservice.Api.csproj"
COPY . .
WORKDIR "/src/UserMicroservice.Api"
RUN dotnet build "UserMicroservice.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UserMicroservice.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserMicroservice.Api.dll"]