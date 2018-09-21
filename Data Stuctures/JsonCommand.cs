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

		public JsonCommand(string name, int args, List<JsonAction> actions)
		{
			this.Name = name;
			this.Arguments = args;
			this.Actions = actions;
		}
    }
}
