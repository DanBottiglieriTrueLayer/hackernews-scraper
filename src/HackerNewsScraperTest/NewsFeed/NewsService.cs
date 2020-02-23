using HackerNewsScraperTest.Models;
using HtmlAgilityPack;
using log4net;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Linq;

namespace HackerNewsScraperTest.NewsFeed
{
    /// <summary>
    /// Servces the news in either DTO or Json form from the desired news source.
    /// </summary>
    public class NewsService
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(NewsService));
        private const int ARTICLES_PER_PAGE = 30;
        private INewsServiceFeed feed;

        public NewsService()
            : this(new NewsServiceFeed())
        {
        }

        public NewsService(INewsServiceFeed newsServiceFeed)
        {
            feed = newsServiceFeed;
        }

        /// <summary>
        /// Serializes the scraped web articles from Hacker news.
        /// </summary>
        /// <param name="posts">The number of posts to retrieve.</param>
        /// <returns>Serialized list of Articles.</returns>
        public IEnumerable<Article> GetNewsFromHackerNews(int posts)
        {
            var articleNodes = new List<HtmlNode>();
            for (int i = 0; articleNodes.Count < posts; i++)
            {
                var htmlDoc = feed.GetNextDocument(i);                

                // Table rows with the class "athing" contain the Title, the URL
                // and the Rank of the article.
                var htmlNodes = htmlDoc.DocumentNode.Descendants("tr")
                    .Where(x => x.Attributes["class"]?.Value == "athing")
                    .Take(posts - i * ARTICLES_PER_PAGE);

                articleNodes.AddRange(htmlNodes);
            }

            return articleNodes.Select(a => a.ToArticle());
        }

        /// <summary>
        /// Serializes into JSON the scraped web articles from Hacker news.
        /// </summary>
        /// <param name="posts">The number of posts to retrieve.</param>
        /// <returns>JSON string of serialized list of Articles.</returns>
        public string GetNewsFromHackerNewsAsJson(int posts)
        {
            var articles = GetNewsFromHackerNews(posts);

            var jsonSettings = new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                },
                Formatting = Formatting.Indented
            };
            return JsonConvert.SerializeObject(articles.ToArray(), jsonSettings);
        }
    }
}
