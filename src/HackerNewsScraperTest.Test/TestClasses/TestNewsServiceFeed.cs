using HackerNewsScraperTest.NewsFeed;
using HtmlAgilityPack;
using log4net;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNewsScraperTest.Test
{
    public class TestNewsServiceFeed : INewsServiceFeed
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(TestNewsServiceFeed));
        public HtmlDocument GetNextDocument(int counter)
        {
            var uri = @$"..\..\..\TestFiles\Pages\testpage{counter + 1}.html";
            var htmlDoc = new HtmlDocument();
            htmlDoc.Load(uri);

            Log.Info($"Scraping {uri}...");
            return htmlDoc;
        }
    }
}
