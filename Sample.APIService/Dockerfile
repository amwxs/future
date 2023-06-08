#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Sample.APIService/Sample.APIService.csproj", "Sample.APIService/"]
RUN dotnet restore "Sample.APIService/Sample.APIService.csproj"
COPY . .
WORKDIR "/src/Sample.APIService"
RUN dotnet build "Sample.APIService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sample.APIService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sample.APIService.dll"]