FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["EPRN.Portal/EPRN.Portal.csproj", "EPRN.Portal/"]
COPY ["EPRN.Common/EPRN.Common.csproj", "EPRN.Common/"]
RUN dotnet restore "EPRN.Portal/EPRN.Portal.csproj"
COPY . .
WORKDIR "/src/Portal"
RUN dotnet build "EPRN.Portal.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EPRN.Portal.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EPRN.Portal.dll"]