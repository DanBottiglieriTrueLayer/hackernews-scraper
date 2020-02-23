# Dan Bottiglieri's TrueLayer Test Entry

This is my submission to TrueLayer's Hacker News scraper test. 

## Prerequisites

These instructions assume you are running on Windows as this is what I built it with.

The application requires .NET Core 3.0 to run. ([Download it here](https://dotnet.microsoft.com/download/dotnet-core/3.0)). You do not need to download it if you have Visual Studio 2019 as it is bundled with it.

If you wish to run the source code, I recommend downloading Visual Studio 2019 ([Download it here](https://visualstudio.microsoft.com/downloads/)). With it, comes Nuget Package Manager to manage the packages in the solution.

If you wish to Dockerise the solution, you will also need to create an account with [Docker](https://www.docker.com) and download then install it. Please note that Docker requires an operating system that supports Hyper-V (for instance, Windows Home does not support this).

## How to run

Navigate to the root of the solution and run:

`nuget restore`

`dotnet publish -c Release`

If either step fails, check your environmental variables and ensure Path has access to both nuget and dotnet.
Once published, run the following commands:

`cd src\HackerNewsScraperTest.Console\bin\Release\netcoreapp3.0\publish`

`hackernews --posts 10`

This will scrape 10 posts from Hacker News as JSON. The 10 can be substituted for any integer between 0 and 100 to pull that many posts.

### Running in Docker

Once the application has been published, navigate to the root folder once more and run the following command:

`docker build -t hackernews -f Dockerfile .`

This creates an image file called 'hackernews'. The image can be run with the following command:

`docker run hackernews --posts 10`

Again, 10 can be substituted with another integer between 0 and 100.

## Libraries Used
|Library                                         |Use                                  
|------------------------------------------------|---------------------------------------------------------------------------------
|HtmlAgilityPack                                 |Library used to read HTML from the site and parse it for the content of interest.    
|Log4net                                         |Easy logging. Can be configured via the log4net.config file.
|Microsoft.Extensions.Configuration              |Used to read the appsettings.json file for a app setting.  
|Microsoft.Extensions.Configuration.Abstractions |Required by Microsoft.Extensions.Configuration.Json.   
|Microsoft.Extensions.Configuration.Json         |Required for Microsoft.Extensions.Configuration to support JSON files.    
|Microsoft.NET.Test.Sdk                          |Required by NUnit for writing tests.   
|Newtonsoft.Json                                 |Serialises objects into JSON and vice versa.   
|NUnit                                           |A test module which allows automated unit testing.
|Nunit3TestAdapter                               |Allows running of the tests created with NUnit within Visual Studio.