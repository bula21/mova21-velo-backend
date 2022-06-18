#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
RUN apt-get update && \
    apt-get install -y wget && \
    apt-get install -y gnupg2 && \
    wget -qO- https://deb.nodesource.com/setup_14.x | bash - && \
    apt-get install -y build-essential nodejs
COPY ["Mova21AppBackend/Mova21AppBackend.csproj", "Mova21AppBackend/"]
RUN dotnet restore "Mova21AppBackend/Mova21AppBackend.csproj"
COPY . .
WORKDIR "/src/Mova21AppBackend"
RUN dotnet build "Mova21AppBackend.csproj" -c Release -o /app/build


FROM build AS publish
RUN dotnet publish "Mova21AppBackend.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mova21AppBackend.dll"]
