using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemeHarvest.Models
{
    public class Meme
    {
        public string Title { get; set; } /* Title or Post ID */
        public string ImageUrl { get; set; } 
        public string Source { get; set; }
        public string SubReddit { get; set; }

        public Meme(string title, string imageUrl, string source, string subReddit)
        {
            Title = title;
            ImageUrl = imageUrl;
            Source = source;
            SubReddit = subReddit;
        }

        public override string ToString()
        {
            return $"{Title}";
        }
    }
}
