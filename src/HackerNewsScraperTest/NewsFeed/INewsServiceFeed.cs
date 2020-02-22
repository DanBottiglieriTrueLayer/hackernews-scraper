using HtmlAgilityPack;

namespace HackerNewsScraperTest.NewsFeed
{
    /// <summary>
    /// An interface used to advance through the pages to scrape.
    /// </summary>
    public interface INewsServiceFeed
    {
        /// <summary>
        /// Uses a counter to track and download a webpage to scrape.
        /// </summary>
        HtmlDocument GetNextDocument(int counter);
    }
}
