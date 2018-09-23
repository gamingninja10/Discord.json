using Newtonsoft.Json;
using System.Collections.Generic;

namespace EzBot.Json
{
    public class JsonEvent
    {
		[JsonProperty("event")]
		public string Name { get; private set; }
		
		[JsonProperty("actions")]
		public List<JsonAction> Actions { get; private set; }
    }
}
