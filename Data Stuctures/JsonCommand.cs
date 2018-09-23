using Newtonsoft.Json;
using System.Collections.Generic;

namespace EzBot.Json
{
    public class JsonCommand
    {
		[JsonProperty("name")]
		public string Name { get; private set; }

		[JsonProperty("args")]
		public int Arguments { get; private set; }

		[JsonProperty("actions")]
		public List<JsonAction> Actions { get; private set; }
    }
}
