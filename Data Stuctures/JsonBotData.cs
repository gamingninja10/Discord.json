using Newtonsoft.Json;
using System.Collections.Generic;

namespace EzBot.Json
{
    public class JsonBotData
    {
		[JsonProperty("token")]
		public string Token { get; private set; }

		[JsonProperty("prefix")]
		public string Prefix { get; private set; }

		[JsonProperty("playing", Required = Required.DisallowNull)]
		public string Playing { get; private set; }
		
		[JsonProperty("activityType", Required = Required.DisallowNull)]
		public int Activity { get; private set; }

		[JsonProperty("mentionPrefix", Required = Required.DisallowNull)]
		public bool MentionPrefix { get; private set; } = true;

		[JsonProperty("commands", Required = Required.DisallowNull)]
		public List<JsonCommand> Commands { get; private set; }

		[JsonProperty("events", Required = Required.DisallowNull)]
		public List<JsonEvent> Events { get; private set; }
	}
}
