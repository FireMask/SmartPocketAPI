# backend/Dockerfile
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# copy csproj(s) and restore first for better caching
COPY SmartPocketAPI.sln SmartPocketAPI/
COPY . .
RUN dotnet restore

# publish
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
COPY --from=build /app/publish ./

ENV PATH="$PATH:/root/.dotnet/tools"
CMD ["dotnet", "SmartPocketAPI.dll"]