using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DndBot.Logging;
using DndBot.MessageHandling;
using DndBot.Token;

namespace DndBot
{
  public class Program
  {
    private DiscordSocketClient _client;
    private CommandService _commands;

    static void Main()
    {
      new Program().MainAsync().GetAwaiter().GetResult();
    }

    private async Task MainAsync()
    {
      ClientLogger logger = new ClientLogger();
      MessageHandler messageHandler = new MessageHandler(_client);
      TokenReader tokenReader = new TokenReader();

      _client = new DiscordSocketClient(new DiscordSocketConfig
      {
        LogLevel = LogSeverity.Debug
      });

      _commands = new CommandService(new CommandServiceConfig
      {
        CaseSensitiveCommands = true,
        DefaultRunMode = RunMode.Async,
        LogLevel = LogSeverity.Debug
      });

      _client.MessageReceived += messageHandler.HandleMessageRecieved;
      await _commands.AddModulesAsync(Assembly.GetEntryAssembly());

      _client.Ready += ClientReady;
      _client.Log += logger.LogToConsole;

      string token = tokenReader.ReadToken();

      await _client.LoginAsync(TokenType.Bot, token);
      await _client.StartAsync();

      await Task.Delay(-1);
    }

    private async Task ClientReady()
    {
      await _client.SetGameAsync("Waiting for the D");
    }

  }
}
