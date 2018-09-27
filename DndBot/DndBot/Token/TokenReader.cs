using System;
using System.IO;

namespace DndBot.Token
{
  /// <summary>
  /// Object to read the token for the client.
  /// </summary>
  public class TokenReader
  {
    private readonly string _token;

    /// <summary>
    /// Constructor. Initializes the default token location.
    /// </summary>
    public TokenReader()
    {
      string programData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
      _token = Path.Combine(programData, "DndDiscordBot", "Tokens", "token.txt");
    }

    /// <summary>
    /// Reads the token string from the program data.
    /// </summary>
    /// <returns>String that is the token.</returns>
    public string ReadToken()
    {
      return File.ReadAllText(_token);
    }
  }
}
