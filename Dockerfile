FROM mcr.microsoft.com/dotnet/core/runtime:3.0

COPY src/HackerNewsScraperTest.Console/bin/Release/netcoreapp3.0/publish hackernews/

ENTRYPOINT ["dotnet", "hackernews/hackernews.dll"]