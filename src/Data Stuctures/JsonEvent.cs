using Newtonsoft.Json;
using System.Collections.Generic;

namespace Discord.json
{
    public class JsonEvent
    {
		[JsonProperty("event")]
		public string Name { get; private set; }
		
		[JsonProperty("actions")]
		public List<JsonAction> Actions { get; private set; }
    }
}
