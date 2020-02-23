using System;
using System.IO;

namespace HackerNewsScraperTest.Models
{
    /// <summary>
    /// A serializable, self-validating model of a Hacker News article.
    /// </summary>
    public class Article
    {
        public Article(string title, string uri, string author, int points, int comments, int rank)
        {
            if (!string.IsNullOrEmpty(title) && title.Length < 256)
                Title = title;
            else
                throw new InvalidDataException($"Title is either empty or over 256 characters long. Title: {title}");

            if (!string.IsNullOrEmpty(author) && author.Length < 256)
                Author = author;
            else
                throw new InvalidDataException($"Author is either empty or over 256 characters long. Author: {author}");

            if (System.Uri.IsWellFormedUriString(uri, UriKind.Absolute))
                Uri = uri;
            else if (System.Uri.IsWellFormedUriString(uri, UriKind.Relative))
                Uri = $@"{AppSettings.BaseUrlToScrape()}/{uri}";
            else
                throw new InvalidDataException($"Uri is not a well formed URI. Uri: {uri}");

            if (rank >= 0)
                Rank = rank;
            else
                throw new InvalidDataException($"Rank cannot be a negative number. Rank: {rank}");

            if (points >= 0)
                Points = points;
            else
                throw new InvalidDataException($"Points cannot be a negative number. Points: {rank}");

            if (comments >= 0)
                Comments = comments;
            else
                throw new InvalidDataException($"Comments cannot be a negative number. Comments: {comments}");
        }
         
        public override string ToString()
        {
            return $"{Rank}. {Title} by {Author} ({Uri}).\n{Points} points, {Comments} comments.";
        }

        /// <summary>
        /// The article title.
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// The URI the article links to.
        /// </summary>
        public string Uri { get; }

        /// <summary>
        /// Who submitted the article.
        /// </summary>
        public string Author { get; }

        /// <summary>
        /// The upvotes - the downvotes on the post.
        /// </summary>
        public int Points { get; }

        /// <summary>
        /// How many comments, if any, are on the post.
        /// </summary>
        public int Comments { get; }

        /// <summary>
        /// The current rank on the list of news items.
        /// </summary>
        public int Rank { get; }
    }
}
