using HackerNewsScraperTest.NewsFeed;
using NUnit.Framework;
using System.Linq;

namespace HackerNewsScraperTest.Test
{
    public class NewsServiceTests
    {
        private NewsService newsService;

        [SetUp]
        public void Setup()
        {
            newsService = new NewsService(new TestNewsServiceFeed());
        }

        [TestCase(100)]
        [TestCase(50)]
        [TestCase(3)]
        [TestCase(1)]
        public void GetNewsWithValidNumbers(int posts)
        {
            var results = newsService.GetNewsFromHackerNews(posts);
            Assert.AreEqual(results.Count(), posts);
            Assert.IsFalse(results.Any(x => x == null));
        }
    }
}