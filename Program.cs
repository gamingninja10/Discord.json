using Newtonsoft.Json;
using System.IO;

namespace EzBot.Json
{
    class Program
    {
        static void Main(string[] args)
        {
			var json = File.ReadAllText($@"{Directory.GetCurrentDirectory()}\bot\bot.json");
			var binds = File.ReadAllText($@"{Directory.GetCurrentDirectory()}\bot\properties.json");
			var botData = JsonConvert.DeserializeObject<JsonBotData>(json);

			var bot = new JBot(botData.Token, botData, binds)
			{
				IsPlaying = true,
				Playing = botData.Playing,
				Prefix = botData.Prefix 
			};
			bot.Start();
        }
    }
}
