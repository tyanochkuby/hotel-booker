using HotelBooker.Interfaces;

namespace HotelBooker.Helpers;

/// <summary>
/// Provides methods to log messages to the console.
/// </summary>
public class ConsoleLogger : ILogger
{
    /// <inheritdoc />
    public void LogInfo(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"INFO: {message}");
        Console.ResetColor();
    }

    /// <inheritdoc />
    public void LogWarning(string message)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"WARNING: {message}");
        Console.ResetColor();
    }

    /// <inheritdoc />
    public void LogError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"ERROR: {message}");
        Console.ResetColor();
    }

    /// <inheritdoc />
    public void LogDebug(string message)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine($"DEBUG: {message}");
        Console.ResetColor();
    }

    public void Log(string message)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(message);
        Console.ResetColor();
    }
}

