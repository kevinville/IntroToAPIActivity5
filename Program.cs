using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace WebAPIClient
{
    class Switchgames
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
                    Console.WriteLine("Enter Switch Game, enter without writing a name to quit.");

                    var switchGame = Console.ReadLine();

                    if (string.IsNullOrEmpty(switchGame))
                    {
                        break;
                    }

                    var result = await client.GetAsync("https://animechan.vercel.app/api/random/anime?title=" + switchGame);
                    var resultRead = await result.Content.ReadAsStringAsync();

                    var switchstuff = JsonConvert.DeserializeObject<Switchgames>(resultRead);

                    Console.WriteLine("---");
                    Console.WriteLine("Anime: " + switchstuff.animu);
                    Console.WriteLine("Character Name: " + switchstuff.character);
                    Console.WriteLine("Quote: " + switchstuff.quote);

                    Console.WriteLine("\n---");
                }
                catch(Exception)
                {
                    Console.WriteLine("Invalid Switch Game.");
                }
            }
        }
    }
}