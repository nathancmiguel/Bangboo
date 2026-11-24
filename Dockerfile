FROM mcr.microsoft.com/dotnet/sdk:10.0

WORKDIR /app

COPY Bot/Bot.csproj Bot/Bot.csproj

RUN dotnet restore Bot/Bot.csproj
COPY Bot/ Bot/
RUN dotnet publish Bot/Bot.csproj -c Release -o out --no-restore

CMD ["dotnet", "out/Bangboo.dll"]
