using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace EzBot.Json
{
    public class JMethodData
    {
		public string Name { get; private set; }
		public List<ParameterInfo> Parameters { get; private set; }

		public JMethodData(string name, List<ParameterInfo> param)
		{
			this.Name = name;
			this.Parameters = param;
		}
    }
}
