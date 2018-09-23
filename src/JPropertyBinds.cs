using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Discord.json
{
    public class JPropertyBinds
    {
		public static Dictionary<string, string> Binds { get; private set; }

		public static void Load(string json)
		{
			try
			{
				Binds = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
			}
			catch (Exception e)
			{
				Console.WriteLine($"Error: {e}");
			}
		}
	}
}
