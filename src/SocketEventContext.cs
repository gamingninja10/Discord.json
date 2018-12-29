using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;

namespace Discord.json
{
	public class SocketEventContext : ICommandContext
	{
		public DiscordSocketClient Client { get; }
		public SocketGuild Guild { get; }
		public ISocketMessageChannel Channel { get; }
		public SocketUser User { get; }
		public SocketUserMessage Message { get; }

		public bool IsPrivate => Channel is IPrivateChannel;

		public SocketEventContext(DiscordSocketClient client, SocketGuild guild)
		{
			Client = client;
			Guild = guild;
			Channel = guild.DefaultChannel;
		}

		public SocketEventContext(DiscordSocketClient client, SocketGuildUser user)
		{
			Client = client;
			Guild = user.Guild;
			Channel = user.Guild.DefaultChannel;
			User = user;
		}

		public SocketEventContext(DiscordSocketClient client, SocketUser user, SocketGuild guild)
		{
			Client = client;
			Guild = guild;
			Channel = guild.DefaultChannel;
			User = user;
		}

		//ICommandContext
		IDiscordClient ICommandContext.Client => Client;
		IGuild ICommandContext.Guild => Guild;
		IMessageChannel ICommandContext.Channel => Channel;
		IUser ICommandContext.User => User;
		IUserMessage ICommandContext.Message => Message;
	}
}
