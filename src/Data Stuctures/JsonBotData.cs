using Newtonsoft.Json;
using System.Collections.Generic;

namespace Discord.json
{
    public class JsonBotData
    {
		// Required
		[JsonProperty("token")]
		public string Token { get; private set; }

		[JsonProperty("prefix")]
		public string Prefix { get; private set; }


		// Not Required

		[JsonProperty("playing", Required = Required.DisallowNull)]
		public string Playing { get; private set; }

		[JsonProperty("streamUrl", Required = Required.DisallowNull)]
		public string StreamUrl { get; private set; }

		[JsonProperty("activity", Required = Required.DisallowNull)]
		public int Activity { get; private set; }

		[JsonProperty("mentionPrefix", Required = Required.DisallowNull)]
		public bool MentionPrefix { get; private set; } = true;

		[JsonProperty("commands", Required = Required.DisallowNull)]
		public List<JsonCommand> Commands { get; private set; }

		[JsonProperty("events", Required = Required.DisallowNull)]
		public List<JsonEvent> Events { get; private set; }
	}
}
