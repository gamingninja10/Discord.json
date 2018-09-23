using Newtonsoft.Json;
using System.IO;

namespace Discord.json
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
				Playing = botData.Playing,
				Prefix = botData.Prefix,
				Activity = (ActivityType)botData.Activity,
				StreamUrl = botData.StreamUrl	
			};
			bot.Start();
        }
    }
}
