using System.Collections.Generic;

namespace EzBot.Json
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
