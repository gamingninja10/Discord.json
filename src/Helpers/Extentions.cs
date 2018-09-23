using System;
using System.Collections.Generic;
using System.Text;

namespace Discord.json.Helpers
{
    public static class Extentions
    {
		public static T ToEnum<T>(this string enumString)
		{
			return (T)Enum.Parse(typeof(T), enumString);
		}
	}
}
