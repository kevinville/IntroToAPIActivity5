using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace WebAPIClient
{
    class AnimeInfo
    {
        [JsonProperty("anime")]
        public string animu { get; set; }
        [JsonProperty("character")]
        public string character { get; set; }

        [JsonProperty("quote")]
        public string quote { get; set; }
        
    }

    class Program
    {
        private static readonly HttpClient client = new HttpClient();


        static async Task Main(string[] args)
        {
            await ProcessRepositories();
        }
        private static async Task ProcessRepositories()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter Anime Name, enter without writing a name to quit.");

                    var animeName = Console.ReadLine();

                    if (string.IsNullOrEmpty(animeName))
                    {
                        break;
                    }

                    var result = await client.GetAsync("https://animechan.vercel.app/api/random/anime?title=" + animeName);
                    var resultRead = await result.Content.ReadAsStringAsync();

                    var animeStuff = JsonConvert.DeserializeObject<AnimeInfo>(resultRead);

                    Console.WriteLine("---");
                    Console.WriteLine("Anime: " + animeStuff.animu);
                    Console.WriteLine("Character Name: " + animeStuff.character);
                    Console.WriteLine("Quote: " + animeStuff.quote);

                    Console.WriteLine("\n---");
                }
                catch(Exception)
                {
                    Console.WriteLine("Invalid Anime Name.");
                }
            }
        }
    }
}