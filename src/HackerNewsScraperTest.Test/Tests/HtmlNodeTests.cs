using HtmlAgilityPack;
using NUnit.Framework;
using HackerNewsScraperTest.NewsFeed;
using System.IO;
using System.Linq;

namespace HackerNewsScraperTest.Test.Tests
{
    public class HtmlNodeTests
    {
        [TestCase("BadUri")]
        [TestCase("LongAuthor")]
        [TestCase("LongTitle")]
        [TestCase("EmptyTitle")]
        [TestCase("NoAuthor")]
        [TestCase("NoUrl")]
        public void BadPosts(string file)
        {
            var uri = @$"..\..\..\TestFiles\HtmlNodes\{file}.html";
            var htmlDoc = new HtmlDocument();
            htmlDoc.Load(uri);
            var node = htmlDoc.DocumentNode.Descendants("tr").First();
            Assert.Throws<InvalidDataException>(() => node.ToArticle());
        }

        [TestCase("CorrectNode")]
        [TestCase("RelativeUrl")]
        public void GoodPosts(string file)
        {
            var uri = @$"..\..\..\TestFiles\HtmlNodes\{file}.html";
            var htmlDoc = new HtmlDocument();
            htmlDoc.Load(uri);
            var node = htmlDoc.DocumentNode.Descendants("tr").First();
            var article = node.ToArticle();
            Assert.IsNotNull(article);
            Assert.IsNotNull(article.Title);
            Assert.IsNotNull(article.Uri);
            Assert.IsTrue(article.Comments >= 0);
            Assert.IsNotNull(article.Author);
            Assert.IsTrue(article.Points >= 0);
            Assert.IsTrue(article.Rank >= 0);
        }
    }
}
