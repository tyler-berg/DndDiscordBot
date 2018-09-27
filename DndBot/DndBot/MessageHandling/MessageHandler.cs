using System;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;

namespace DndBot.MessageHandling
{
  /// <summary>
  /// Object for handling messages.
  /// </summary>
  public class MessageHandler
  {
    private readonly DiscordSocketClient _client;

    public MessageHandler(DiscordSocketClient client)
    {
      _client = client;
    }

    /// <summary>
    /// Handles the given message.
    /// </summary>
    /// <param name="socketMessage">Message to handle.</param>
    /// <returns></returns>
    public async Task HandleMessageRecieved(SocketMessage socketMessage)
    {
      await Task.Yield();

      var message = socketMessage as SocketUserMessage;
      var context = new SocketCommandContext(_client, message);

      if (context.Message.Content.StartsWith('!') && !context.User.IsBot)
      {
        HandleMessage(context);
      }
    }

    private void HandleMessage(SocketCommandContext context)
    {
      var messageContents = context.Message.Content.Split(' ');
      if (messageContents.First() == "!r" || messageContents.First() == "!roll")
      {
        HandleRollRommand(messageContents, context.User, context.Channel);
      }
    }

    private void HandleRollRommand(string[] parsedMessage, SocketUser user, ISocketMessageChannel channel)
    {
      if (parsedMessage == null || parsedMessage.Length < 1)
      {
        throw new ArgumentException("Cannot parse no strings");
      }

      if (parsedMessage.Length == 1)
      {
        string message = $"{user.Username} rolled 1d20: {Roll(20)}";
        channel.SendMessageAsync(message);
      }
    }

    private int Roll(int max)
    {
      var random = new Random();
      return random.Next(1, max);
    }
  }
}
