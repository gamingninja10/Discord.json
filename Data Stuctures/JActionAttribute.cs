using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace EzBot.Json
{
    public class JActionAttribute : Attribute
    {
		public string Name { get; set; }

		public JActionAttribute(string name)
		{
			this.Name = name;
		}
    }
}
