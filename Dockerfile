FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

COPY . .

RUN dotnet restore
RUN dotnet build -c Release --no-restore

RUN dotnet publish -c Release -o out --no-build

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

CMD ["dotnet", "BaseNet.Api.dll"]
