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

		public JsonAction(string action, List<string> args)
		{
			this.Name = action;
			this.Arguments = args;
		}
    }
}
