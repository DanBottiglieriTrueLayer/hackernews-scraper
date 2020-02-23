using HtmlAgilityPack;
using log4net;

namespace HackerNewsScraperTest.NewsFeed
{
    /// <summary>
    /// The default implementation of the news feed, which gets
    /// the URL from the app settings and scrapes it one page at a time.
    /// </summary>
    public class NewsServiceFeed : INewsServiceFeed
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(NewsServiceFeed));
        
        /// <summary>
        /// Returns a HtmlDocument representation of a Hacker News page.
        /// </summary>
        /// <param name="counter">The page to return.</param>
        public HtmlDocument GetNextDocument(int counter)
        {
            var url = @$"{AppSettings.BaseUrlToScrape()}/news?p={counter + 1}";
            var htmlDoc = new HtmlWeb().Load(url);

            Log.Info($"Scraping {url}...");
            return htmlDoc;
        }
    }
}
