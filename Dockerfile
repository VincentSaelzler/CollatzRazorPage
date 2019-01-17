FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["CollatzCoreRazorPage.csproj", "./"]
RUN dotnet restore "./CollatzCoreRazorPage.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "CollatzCoreRazorPage.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "CollatzCoreRazorPage.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "CollatzCoreRazorPage.dll"]