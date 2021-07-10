#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["O10.Nomy/O10.Nomy.csproj", "O10.Nomy/"]
RUN dotnet restore "O10.Nomy/O10.Nomy.csproj"
COPY . .
WORKDIR "/src/O10.Nomy"
RUN dotnet build "O10.Nomy.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "O10.Nomy.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "O10.Nomy.dll"]