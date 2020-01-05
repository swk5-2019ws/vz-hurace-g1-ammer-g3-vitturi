FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Hurace.Api/Hurace.Api.csproj", "Hurace.Api/"]
COPY ["Hurace.Core/Hurace.Core.csproj", "Hurace.Core/"]
COPY ["Hurace.Core.Interface/Hurace.Core.Interface.csproj", "Hurace.Core.Interface/"]
COPY ["Hurace.Mapper/Hurace.Core.Mapper.csproj", "Hurace.Mapper/"]
COPY ["Hurace.Simulator/Hurace.Simulator.csproj", "Hurace.Simulator/"]
COPY ["Hurace.Timer/Hurace.Timer.csproj", "Hurace.Timer/"]
COPY ["Hurace.Domain/Hurace.Domain.csproj", "Hurace.Domain/"]
RUN ls -lah
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