FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BBBv2/BBBv2.csproj", "BBBv2/"]
RUN dotnet restore "BBBv2/BBBv2.csproj"
COPY . .
WORKDIR "/src/BBBv2"
RUN dotnet build "BBBv2.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BBBv2.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BBBv2.dll"]
