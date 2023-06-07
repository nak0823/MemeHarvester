using MemeHarvest.Models;
using MemeHarvest.Utils;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace MemeHarvest.Services
{
    internal class Reddit
    {
        private readonly HttpClient _httpClient;
        private readonly string _postIdsFilePath;

        public Reddit(string postIdsFilePath)
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "Serialized's Meme Harvester");
            _postIdsFilePath = postIdsFilePath;
        }

        public async Task<List<Meme>> ScrapeMeme(string subReddit, int amount)
        {
            FileHandler fileHandler = new FileHandler();

            string reqUrl = $"https://www.reddit.com/r/{subReddit}/.json?limit={amount}";

            HttpResponseMessage response = await _httpClient.GetAsync(reqUrl);

            if (response.IsSuccessStatusCode)
            {
                string jsonContent = await response.Content.ReadAsStringAsync();
                dynamic data = JsonConvert.DeserializeObject<dynamic>(jsonContent);

                List<Meme> harvestedMemes = new List<Meme>();

                foreach (var post in data.data.children)
                {
                    string postId = post.data.id.ToString();

                    if (!IsPostIdExists(postId))
                    {
                        string title = post.data.title.ToString();
                        string imageUrl = post.data.url.ToString();

                        Meme meme = new Meme(title, imageUrl, "Reddit", subReddit);
                        harvestedMemes.Add(meme);

                        await fileHandler.DownloadImage(meme);
                        Program.Logger.Log(meme.ToString(), true);

                        SavePostId(postId);
                    }
                    else
                    {
                        Program.Logger.Log($" Meme is Already Downloaded Id: [{postId}]", true);
                    }
                }

                return harvestedMemes;
            }

            Program.Logger.Log(" No memes have been Harvested.", true);
            return new List<Meme>();
        }

        private bool IsPostIdExists(string postId)
        {
            if (File.Exists(_postIdsFilePath))
            {
                string[] existingPostIds = File.ReadAllLines(_postIdsFilePath);
                foreach (string existingPostId in existingPostIds)
                {
                    if (existingPostId == postId)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private void SavePostId(string postId)
        {
            File.AppendAllText(_postIdsFilePath, postId + "\n");
        }
    }
}