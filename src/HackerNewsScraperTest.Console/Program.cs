using HackerNewsScraperTest.NewsFeed;
using log4net;
using log4net.Config;
using System;
using System.IO;
using System.Reflection;

namespace HackerNewsScraperTest.Console
{
    class Program
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(Program));        

        static void Main(string[] args)
        {
            // Set up log4net console logging
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

            var numberOfPostsToPull = 0;
            try
            {
                numberOfPostsToPull = ValidateInput(args);
            }
            catch (Exception ex)
            {
                // Bad input: exit with bad command line arguments code.
                Log.Error(ex.Message);
                Environment.Exit(0x667);
            }

            var newsService = new NewsService();
            var json = newsService.GetNewsFromHackerNewsAsJson(numberOfPostsToPull);

            System.Console.Write(json);
            Environment.Exit(0);
        }

        /// <summary>
        /// Retrieves useful information from the command line
        /// arguments and throws exceptions if malformed.
        /// </summary>
        /// <param name="args">Arguments meant to be passed from the command line.</param>
        /// <returns>The number of posts to retrieve from the input.</returns>
        private static int ValidateInput(string[] args)
        {
            if (args == null || args.Length != 2 || args[0] != "--posts")
                throw new ArgumentException("Please use the following syntax: hackernews --posts n\nWhere n is an integer from 1-100.");
            if (!int.TryParse(args[1], out int posts) || posts <= 0 || posts > 100)
                throw new ArgumentException("Please enter a valid integer from 1-100 after --posts.");
            else
                return posts;
        }
    }
}
