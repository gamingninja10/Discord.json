using System;

namespace Discord.json
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
