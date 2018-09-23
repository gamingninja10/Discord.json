using Newtonsoft.Json;
using System.Collections.Generic;

namespace Discord.json
{
    public class JsonAction
    {
		[JsonProperty("action")]
		public string Name { get; set; }

		[JsonProperty("args")]
		public List<string> Arguments { get; set; }
    }
}
