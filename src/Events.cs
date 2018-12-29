using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Discord.json
{
	public class Events
	{
		public static JBot Bot { get; set; }

		public async static Task UserJoined(SocketGuildUser user)
		{
			await Bot.ExecuteActionsAsync("UserJoined", user);
		}
		public async static Task UserBanned(SocketUser user, SocketGuild guild)
		{
			await Bot.ExecuteActionsAsync("UserBanned", user, guild);
		}
		public async static Task UserLeft(SocketGuildUser user)
		{
			await Bot.ExecuteActionsAsync("UserLeft", user);
		}
	}
}
