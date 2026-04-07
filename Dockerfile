# quizApi — ASP.NET Core 10 (Render.com Web Service için)
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY quizApi.csproj .
RUN dotnet restore quizApi.csproj

COPY . .
RUN dotnet publish quizApi.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_ENVIRONMENT=Production
# Render çalışma anında PORT atar; yoksa appsettings.json (5003) geçerlidir.
EXPOSE 8080

ENTRYPOINT ["dotnet", "quizApi.dll"]
