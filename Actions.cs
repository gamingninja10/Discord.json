using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EzBot.Json
{
	public class Actions
	{
		public static SocketCommandContext Context { get; set; }
		public Dictionary<string, JMethodData> Commands { get; set; }

		[JAction("Reply")]
		public async Task SendMessage(string text) => await ReplyAsync(text);

		[JAction("Purge")]
		public async Task PurgeAsync(string amountString)
		{
			if (Int32.TryParse(amountString, out int amount))
			{
				var messages = Context.Channel.GetMessagesAsync((amount + 1)).Flatten().ToEnumerable();
				await (Context.Channel as SocketTextChannel).DeleteMessagesAsync(messages);
			}
		}

		[JAction("AddRole"), RequireBotPermission(GuildPermission.ManageRoles)]
		public async Task AddRoleAsync(string username, string roleName)
		{
			var user = Context.Guild.Users.ToList().Find(u => u.Username == username);
			var role = Context.Guild.Roles.ToList().Find(r => r.Name == roleName);

			await user.AddRoleAsync(role);
		}

		[JAction("CreateRole")]
		public async Task CreateRoleAsync(string name) => await Context.Guild.CreateRoleAsync(name);

		[JAction("DeleteRole")]
		public async Task DeleteRoleAsync(string name) => await Context.Guild.Roles.ToList().Find(r => r.Name == name).DeleteAsync();

		[JAction("CreateChannel")]
		public async Task CreateChannel(string name)
		{
			await Context.Guild.CreateTextChannelAsync(name);
		}

		[JAction("DeleteChannel")]
		public async Task DeleteChannel(string name)
		{
			var channel = Context.Guild.Channels.ToList().Find(c => c.Name == name);
			await channel.DeleteAsync();
		}

		[JAction("ChangeNickname")]
		public async Task ChangeNicknameAsync(string userString, string name)
		{
			var user = Context.Guild.Users.ToList().Find(u => u.Username == userString);
			Console.WriteLine(user.Username);
			await user.ModifyAsync(n => n.Nickname = name);
		}

		[JAction("MoveToCategory")]
		public async Task MoveChannel(string channelName, string categoryName)
		{
			try
			{
				var channel = Context.Guild.Channels.ToList().Find(c => c.Name == channelName);
				var category = Context.Guild.CategoryChannels.ToList().Find(c => c.Name == categoryName);

				await channel.ModifyAsync(x => x.CategoryId = category.Id);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		public async Task ExecuteAsync(SocketCommandContext context, JsonCommand command, CommandArgs cmdData)
		{
			Context = context;

			foreach (var action in command.Actions)
			{
				var localArgs = ArgParser(action.Arguments, cmdData);

				var commandMethod = Commands[action.Name];
				try
				{
					//Checks if the parameters are the correct legnth
					if (localArgs.Count > commandMethod.Parameters.Count)
					{
						var amount = localArgs.Count - commandMethod.Parameters.Count;
						localArgs.RemoveRange(commandMethod.Parameters.Count, amount);
					}
					else if (localArgs.Count < commandMethod.Parameters.Count)
					{
						await ReplyAsync("Not Enough Parameters");
					}

					await ReflectionHelper.InvokeMethod<Actions>(new Actions(), commandMethod.Name, localArgs.ToArray());
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
				}
			}
		}

		private List<object> ArgParser(List<string> actionArgs, CommandArgs userArgs)
		{
			var returnArgs = new List<object>();

			foreach (string _arg in actionArgs)
			{
				// Binds the command arguments - Exmaple: !kick CoolGuy365 "This is a reason", action: kick, args: |0| -> 'CoolGuy365', |1| -> 'This is a reason'
				var regex = new Regex(@"^\|(.*?)\|$");
				var matches = regex.Matches(_arg);

				if (matches.Count > 0)
				{
					var arg = matches[0].Groups[1].Value;
					Console.WriteLine(arg);

					if (Int32.TryParse(arg, out int index))
						returnArgs.Add(userArgs.Arguments[index]);
					else
					{
						var bindArg = ReflectionHelper.GetPropValue<string>(this, JPropertyBinds.Binds[arg]);
						returnArgs.Add(bindArg);
					}
				}
				else
					returnArgs.Add(_arg);
			}
			return returnArgs;
		}

		private async Task ReplyAsync(string text, bool isTTS = false, Embed embed = null)
		{
				await Context.Channel.SendMessageAsync(text, isTTS, embed);
		}
	}
}
