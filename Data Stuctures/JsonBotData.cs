using Newtonsoft.Json;
using System.Collections.Generic;

namespace EzBot.Json
{
    public class JsonBotData
    {
		[JsonProperty("token")]
		public string Token { get; private set; }

		[JsonProperty("playing")]
		public string Playing { get; private set; }

		[JsonProperty("prefix")]
		public string Prefix { get; private set; }

		[JsonProperty("commands")]
		public List<JsonCommand> Commands { get; private set; }

		public JsonBotData(string token, string playing, string prefix, List<JsonCommand> commands)
		{
			this.Token = token;
			this.Playing = playing;
			this.Prefix = prefix;
			this.Commands = commands;
		}
    }
}
