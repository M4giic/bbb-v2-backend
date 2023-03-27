FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["OperationTagsService/OperationTagsService.csproj", "OperationTagsService/"]
RUN dotnet restore "OperationTagsService/OperationTagsService.csproj"
COPY . .
WORKDIR "/src/OperationTagsService"
RUN dotnet build "OperationTagsService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OperationTagsService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OperationTagsService.dll"]
