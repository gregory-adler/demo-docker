FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 5010

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
WORKDIR /src
COPY mvcdocker/mvcdocker.csproj mvcdocker/
RUN dotnet restore "mvcdocker/mvcdocker.csproj"
COPY . .
WORKDIR "/src/mvcdocker"
RUN dotnet build "mvcdocker.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "mvcdocker.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "mvcdocker.dll"]
