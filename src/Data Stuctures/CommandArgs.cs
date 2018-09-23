using System.Collections.Generic;

namespace Discord.json
{
    public class CommandArgs
    {
		public string Command { get; private set; }
		public List<object> Arguments { get; private set; }

		public CommandArgs(string command, List<object> args)
		{
			this.Command = command;
			this.Arguments = args;
		}
    }
}
