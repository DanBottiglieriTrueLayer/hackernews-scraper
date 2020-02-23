using Microsoft.Extensions.Configuration;

namespace HackerNewsScraperTest
{
    public static class AppSettings
    {
        private static IConfigurationRoot configuration;
        private static void Initialise()
        {
            configuration = new ConfigurationBuilder()
                              .AddJsonFile("appsettings.json")
                              .Build();
        }

        /// <summary>
        /// The URL which is scraped by the application.
        /// </summary>
        /// <returns></returns>
        public static string BaseUrlToScrape()
        {
            if (configuration == null)
                Initialise();
            return configuration["appSettings:BaseUrlToScrape"];
        }
    }
}
