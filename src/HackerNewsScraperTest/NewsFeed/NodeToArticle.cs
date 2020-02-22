using HackerNewsScraperTest.Models;
using HtmlAgilityPack;
using log4net;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace HackerNewsScraperTest.NewsFeed
{
    /// <summary>
    /// Transforms a HTML document into an Article DTO.
    /// </summary>
    public static class NodeToArticle
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(NodeToArticle));

        /// <summary>
        /// Extention method used to transform a HtmlNode into a fully serialized Hacker News article.
        /// </summary>
        public static Article ToArticle(this HtmlNode athingNode)
        {
            var pointsAuthorCommentsNode = GetSiblingCell(athingNode);

            var rank = ScrapeNumber(athingNode, "span", "rank");
            var title = ScrapeNode(athingNode, "a", "storylink").InnerText;
            var uri = ScrapeNode(athingNode, "a", "storylink").Attributes["href"]?.Value;
            var points = ScrapeNumber(pointsAuthorCommentsNode, "span", "score");
            var author = ScrapeNode(pointsAuthorCommentsNode, "a", "hnuser")?.InnerText;

            var commentsNode = pointsAuthorCommentsNode.Descendants("a").FirstOrDefault(x => x.InnerText.Contains("comments"));
            var comments = GetNumberFromString(commentsNode?.InnerText);

            try
            {
                var article = new Article(title, uri, author, points, comments, rank);
                Log.Info($"Created: {article}");
                return article;
            }
            catch (Exception ex)
            {
                Log.Error($"Error creating article: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Uses Regex to retrieve a number from the input, or 0 if its empty.
        /// </summary>
        private static int GetNumberFromString(string input)
        {
            if (string.IsNullOrEmpty(input))
                return 0;

            var regex = new Regex("[0-9]+");
            return int.Parse(regex.Match(input).Value);
        }

        /// <summary>
        /// Scrapes the useful parts of the node.
        /// </summary>
        /// <param name="node">The node to scrape.</param>
        /// <param name="decendentTag">The tag which is of interest.</param>
        /// <param name="classSelector">The class name which has the information of interest.</param>
        /// <returns>The scraped HTML node which contains the required information.</returns>
        private static HtmlNode ScrapeNode(HtmlNode node, string decendentTag, string classSelector)
        {
            return node.Descendants(decendentTag)
                .FirstOrDefault(x => x.Attributes["class"]?.Value == classSelector);
        }

        /// <summary>
        /// Scrapes a useful number from the node.
        /// </summary>
        /// <param name="node">The node to scrape.</param>
        /// <param name="decendentTag">The tag which is of interest.</param>
        /// <param name="classSelector">The class name which has the information of interest.</param>
        /// <returns>The scraped number from the inner text of the HTML node which contains the required information.</returns>
        private static int ScrapeNumber(HtmlNode node, string decendentTag, string classSelector)
        {
            return GetNumberFromString(ScrapeNode(node, decendentTag, classSelector)?.InnerText);
        }
        
        /// <summary>
        /// Gets the next \<tr\>\<\/tr\> node, which contains Author, Comments and Points.
        /// </summary>
        private static HtmlNode GetSiblingCell(HtmlNode node)
        {
            var nextSibling = node.NextSibling;

            if (nextSibling?.Name != "tr")
                return GetSiblingCell(nextSibling);
            else
                return nextSibling;
        }
    }
}
