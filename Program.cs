using MemeHarvest.Models;
using MemeHarvest.Services;
using MemeHarvest.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace MemeHarvest
{
    internal class Program
    {
        public static Logger Logger = new Logger("Logs.txt");

        public static async Task Main()
        {
            Art.PrintLogo();
            Console.Title = "MemeHarvester by Serialized";

            Reddit redditScraper = new Reddit("PostIds.txt");

            Colorful.Console.Write("\n Time to scrape: r/", Color.Aqua);
            string subreddit = Console.ReadLine();

            Colorful.Console.Write(" Amount to scrape #", Color.Aqua);
            var amountStr = int.TryParse(Console.ReadLine(), out int memesAmount);

            try
            {
                List<Meme> scrapedMemes = await redditScraper.ScrapeMeme(subreddit.ToString(), memesAmount);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, false);
            }

            Colorful.Console.WriteLine($" Finished Downloading Memes from r/{subreddit}.", Color.HotPink);
            Console.ReadKey();
            Console.Clear();
            await Main();
        }
    }
}