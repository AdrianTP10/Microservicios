#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["PaymentMicroservice.Api/PaymentMicroservice.Api.csproj", "PaymentMicroservice.Api/"]
COPY ["PaymentMicroservice.ApplicationServices/PaymentMicroservice.ApplicationServices.csproj", "PaymentMicroservice.ApplicationServices/"]
COPY ["PaymentMicroservice.Core/PaymentMicroservice.Core.csproj", "PaymentMicroservice.Core/"]
COPY ["PaymentMicroservice.DataAccess/PaymentMicroservice.DataAccess.csproj", "PaymentMicroservice.DataAccess/"]
COPY ["PaymentMicroservice.Shared/PaymentMicroservice.Shared.csproj", "PaymentMicroservice.Shared/"]
RUN dotnet restore "PaymentMicroservice.Api/PaymentMicroservice.Api.csproj"
COPY . .
WORKDIR "/src/PaymentMicroservice.Api"
RUN dotnet build "PaymentMicroservice.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PaymentMicroservice.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PaymentMicroservice.Api.dll"]