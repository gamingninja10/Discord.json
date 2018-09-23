using Newtonsoft.Json;
using System.Collections.Generic;

namespace EzBot.Json
{
    public class JsonAction
    {
		[JsonProperty("action")]
		public string Name { get; set; }

		[JsonProperty("args")]
		public List<string> Arguments { get; set; }
    }
}
