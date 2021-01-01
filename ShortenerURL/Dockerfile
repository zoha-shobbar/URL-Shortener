FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
WORKDIR /src
COPY ["ShortenerURL/ShortenerURL.csproj", "ShortenerURL/"]
RUN dotnet restore "ShortenerURL/ShortenerURL.csproj"
COPY . .
WORKDIR "/src/ShortenerURL"
RUN dotnet build "ShortenerURL.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ShortenerURL.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShortenerURL.dll"]