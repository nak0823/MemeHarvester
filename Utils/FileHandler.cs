using MemeHarvest.Models;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace MemeHarvest.Utils
{
    internal class FileHandler
    {
        public async Task DownloadImage(Meme meme)
        {
            if (!Directory.Exists($"{AppDomain.CurrentDomain.BaseDirectory}\\{meme.Source}\\{meme.SubReddit}"))
                Directory.CreateDirectory($"{AppDomain.CurrentDomain.BaseDirectory}\\{meme.Source}\\{meme.SubReddit}");

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    byte[] imageBytes = await client.GetByteArrayAsync(meme.ImageUrl);

                    string title = meme.Title;

                    // Remove invalid characters so we can save the meme under the original title name
                    foreach (char invalidChar in Path.GetInvalidFileNameChars())
                    {
                        title = title.Replace(invalidChar.ToString(), string.Empty);
                    }

                    // Use the first 50 characters of the memes title
                    int maxTitleLength = 50;

                    if (title.Length > maxTitleLength)
                    {
                        title = title.Substring(0, maxTitleLength);
                    }

                    string fileExtension = Path.GetExtension(meme.ImageUrl);

                    string fileName = $"{Guid.NewGuid()}{title}{fileExtension}";

                    string filePath = Path.Combine($"{AppDomain.CurrentDomain.BaseDirectory}\\{meme.Source}\\{meme.SubReddit}", fileName);
                    File.WriteAllBytes(filePath, imageBytes);

                    Program.Logger.Log($"Saved Image: {meme.Title} in ${filePath}", false);
                }
            }
            catch (Exception ex)
            {
                Program.Logger.Log($"Error downloading meme image: {ex.Message}", true);
            }
        }
    }
}