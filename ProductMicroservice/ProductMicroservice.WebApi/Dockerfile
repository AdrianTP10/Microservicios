#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ProductMicroservice.WebApi/ProductMicroservice.WebApi.csproj", "ProductMicroservice.WebApi/"]
COPY ["ProductMicroservice.ApplicationServices/ProductMicroservice.ApplicationServices.csproj", "ProductMicroservice.ApplicationServices/"]
COPY ["ProductMicroservice.Core/ProductMicroservice.Core.csproj", "ProductMicroservice.Core/"]
COPY ["ProductMicroservice.DataAccess/ProductMicroservice.DataAccess.csproj", "ProductMicroservice.DataAccess/"]
RUN dotnet restore "ProductMicroservice.WebApi/ProductMicroservice.WebApi.csproj"
COPY . .
WORKDIR "/src/ProductMicroservice.WebApi"
RUN dotnet build "ProductMicroservice.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProductMicroservice.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductMicroservice.WebApi.dll"]