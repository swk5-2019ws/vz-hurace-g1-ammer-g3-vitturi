FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["Hurace.Api/Hurace.Api.csproj", "Hurace.Api/"]
RUN dotnet restore "Hurace.Api/Hurace.Api.csproj"
COPY . .
WORKDIR "/src/Hurace.Api"
RUN dotnet build "Hurace.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Hurace.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Hurace.Api.dll"]