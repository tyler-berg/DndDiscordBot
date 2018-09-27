using System;
using System.Threading.Tasks;
using Discord;

namespace DndBot.Logging
{
  /// <summary>
  /// Object for handling client logging.
  /// </summary>
  public class ClientLogger
  {
    /// <summary>
    /// Logs the given message to the console.
    /// </summary>
    /// <param name="logMessage">Message to log to the console.</param>
    /// <returns></returns>
    public async Task LogToConsole(LogMessage logMessage)
    {
      await Task.Yield();
      Console.WriteLine($"[{DateTime.Now} from {logMessage.Source}] {logMessage.Message}");
    }
  }
}
