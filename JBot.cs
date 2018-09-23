using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EzBot.Json
{
	public class JBot
	{
		/// <summary>
		/// Command prefix for the bot
		/// </summary>
		public string Prefix { get; set; }

		/// <summary>
		/// Enables or disables mentiong the bot to trigger a command
		/// </summary>
		public bool AllowMentionPrefix { get; set; } = true;

		/// <summary>
		/// Allows you to enable or disable the discord bots log
		/// </summary>
		public bool PrintLog { get; set; } = true;

		/// <summary>
		/// Allows you to set the token type of the bot (Leave this default for most sitations)
		/// </summary>
		public TokenType TokenType { get; set; } = TokenType.Bot;

		/// <summary>
		/// The collection of services the bot uses. NOTE: Add the services before starting the bot
		/// </summary>
		public ServiceCollection Services { get; set; } = new ServiceCollection();

		/// <summary>
		/// Allows the bot to display Playing/Listening/Streaming/Watching under the name
		/// </summary>
		public bool IsPlaying { get; set; } = false;

		/// <summary>
		/// The name/text of the thing the bot is Playing/Listening/Streaming/Watching
		/// </summary>
		public string Playing { get; set; } = "null";

		/// <summary>
		/// Set the activity of the bot
		/// </summary>
		public ActivityType Activity { get; set; } = ActivityType.Playing;

		/// <summary>
		/// When the activity is 'Streaming' this sets the link of the stream. NOTE: Only works with twitch
		/// </summary>
		public string StreamUrl { get; set; } = null;

		private readonly string _token;
		private readonly JsonBotData _data;
		private readonly string _binds;

		/// <summary>
		/// Creates a new bot instance
		/// </summary>
		/// <param name="token">The user token of the bot. NOTE: If you're using a User/Webhook/Bearer make sure to change the token type in the bot properties</param>
		public JBot(string token, JsonBotData data, string binds)
		{
			_token = token;
			_data = data;
			_binds = binds;
		}

		/// <summary>
		/// The base client of the discord bot
		/// </summary>
		public DiscordSocketClient Client;
		private Actions _actions;
		private IServiceProvider _services;

		public void Start()
		{
			RunBotAsync().GetAwaiter().GetResult();
		}

		private async Task RunBotAsync()
		{
			Client = new DiscordSocketClient();
			_actions = new Actions();

			_services = Services
				.AddSingleton(Client)
				.BuildServiceProvider();

			Client.Log += Log;

			if (IsPlaying)
				await Client.SetGameAsync(Playing, StreamUrl, Activity);

			// Register Commands
			Client.MessageReceived += HandleCommandAsync;

			RegisterCommands();

			// Log in as bot
			await Client.LoginAsync(TokenType, _token);

			// Start the bot client
			await Client.StartAsync();

			// Stop console from closing
			await Task.Delay(-1);
		}

		private void RegisterCommands()
		{
			JPropertyBinds.Load(_binds);

			_actions.JActions = new Dictionary<string, JMethodData>();
			var Commands = ReflectionHelper.GetMethodsWithAttribute(typeof(Actions), typeof(JActionAttribute));

			foreach (var command in Commands)
			{
				var jcmd = (JActionAttribute)command.GetCustomAttribute(typeof(JActionAttribute));
				_actions.JActions.Add(jcmd.Name, new JMethodData(command.Name, command.GetParameters().ToList()));
			}
		}

		private Task Log(LogMessage arg)
		{
			if (PrintLog)
				Console.WriteLine(arg);

			return Task.CompletedTask;
		}

		private async Task HandleCommandAsync(SocketMessage arg)
		{
			if (!(arg is SocketUserMessage message) || message.Author.IsBot) return;

			int argPos = 0;
			if (message.HasStringPrefix(Prefix, ref argPos) || message.HasMentionPrefix(Client.CurrentUser, ref argPos) && AllowMentionPrefix)
			{
				var context = new SocketCommandContext(Client, message);
				var commandArgs = ParseMessage(message.Content);
				await _actions.ExecuteAsync(context, GetCommand(commandArgs.Command), commandArgs);
			}
		}

		public CommandArgs ParseMessage(string content)
		{
			var regex = new Regex(@"[\""].+?[\""]|[^ ]+|^"); // This pattern separates everything via spaces, unless it's in single or double quotes.
			var matches = regex.Matches(content).ToList();

			var command = matches[0].Value.Replace(Prefix, ""); // Removes the prefix from the command
			var parameters = new List<object>();

			for (int i = 1; i < matches.Count; i++)
				parameters.Add(matches[i].Value.Replace("\"", "")); // Removes the quotes from the strings

			return new CommandArgs(command, parameters);
		}

		public JsonCommand GetCommand(string command)
		{
			try
			{
				var returnCommand = _data.Commands.Find(c => c.Name == command);
				return returnCommand;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				Console.WriteLine("Invaild Command");
				return null;
			}
		}
	}
}
