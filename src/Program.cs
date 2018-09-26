using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;

namespace Discord.json
{
    class Program
    {
        static void Main(string[] args)
        {
			var files = Directory.GetFiles($@"{Directory.GetCurrentDirectory()}\bot").ToList();
			var json = File.ReadAllText(files.Where(f => f.Contains(Path.GetExtension(".bot"))).FirstOrDefault());
			var binds = File.ReadAllText(files.Where(f => f.Contains(Path.GetExtension(".binds"))).FirstOrDefault());

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
