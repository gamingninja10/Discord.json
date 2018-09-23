using Discord.Commands;
using Discord.json.Helpers;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Discord.json
{
	public class Actions
	{
		// Holds information about the guild, message, user, bot etc
		public static SocketCommandContext Context { get; set; }

		// This is a list of all vaild jactions
		public Dictionary<string, JMethodData> JActions { get; set; }

		[JAction("Reply")]
		public async Task SendMessage(string text) => await ReplyAsync(text);

		[JAction("Purge"), RequireBotPermission(GuildPermission.ManageMessages)]
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

		[JAction("CreateRole"), RequireBotPermission(GuildPermission.ManageRoles)]
		public async Task CreateRoleAsync(string name) => await Context.Guild.CreateRoleAsync(name);

		[JAction("DeleteRole"), RequireBotPermission(GuildPermission.ManageRoles)]
		public async Task DeleteRoleAsync(string name) => await Context.Guild.Roles.ToList().Find(r => r.Name == name).DeleteAsync();

		[JAction("CreateChannel"), RequireBotPermission(GuildPermission.ManageChannels)]
		public async Task CreateChannel(string name)
		{
			await Context.Guild.CreateTextChannelAsync(name);
		}

		[JAction("DeleteChannel"), RequireBotPermission(GuildPermission.ManageChannels)]
		public async Task DeleteChannel(string name)
		{
			var channel = Context.Guild.Channels.ToList().Find(c => c.Name == name);
			await channel.DeleteAsync();
		}

		[JAction("ChangeNickname")]
		public async Task ChangeNicknameAsync(string userString, string name)
		{
			var user = Context.Guild.Users.ToList().Find(u => u.Username == userString);
			await user.ModifyAsync(n => n.Nickname = name);
		}

		[JAction("MoveToCategory"), RequireBotPermission(GuildPermission.ManageChannels)]
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

				var commandMethod = JActions[action.Name];
				try
				{
					// Checks if the parameters are the correct legnth to avoid crashes
					if (localArgs.Count > commandMethod.Parameters.Count)
					{
						// Gets the amount of extra paramerts then removes those from the end of the list
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
				// Binds the command arguments
				// Exmaple: !kick CoolGuy365 "This is a reason", action: kick, args: |0| -> 'CoolGuy365', | | -> 'This is a reason'
				// Example: !guildinfo, action: reply, args: "Name: |guild| \n Members: |guild.members|"
				var regex = new Regex(@"\|(.*?)\|"); // This pattern matches everything between ||
				var matches = regex.Matches(_arg);

				// Checks if there is any matches
				// If there is go through each one and replace it with the correct information
				// If it's a number, it replaces it with that position in the command parameters. 
				// Example: Command: !echo hi, Action: Reply "Echo: |0|", Returns "Echo: hi"
				// If it's a string it replaces it with the proper property so, "Welcome to '|guild|'" -> "Welcome to 'MyCoolGuild'"
				if (matches.Count > 0)
				{
					string returnArg = _arg;
					foreach (Match match in matches)
					{
						var arg = match.Groups[1].Value;
						
						if (Int32.TryParse(arg, out int index))
						{
							var posArg = userArgs.Arguments[index];
							returnArg = returnArg.Replace(match.Value, posArg.ToString());
						}
						else
						{
							var bindArg = ReflectionHelper.GetPropValue<string>(this, JPropertyBinds.Binds[arg]);
							returnArg = returnArg.Replace(match.Value, bindArg);
						}
					}
					returnArgs.Add(returnArg);

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
