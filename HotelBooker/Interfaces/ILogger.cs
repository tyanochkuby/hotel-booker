namespace HotelBooker.Interfaces;

/// <summary>
/// Interface for logging.
/// </summary>
public interface ILogger
{
    /// <summary>
    /// Logs a message.
    /// </summary>
    /// <param name="message">The message to log.</param>
    void Log(string message);


    /// <summary>
    /// Logs an informational message
    /// </summary>
    /// <param name="message">The message to log.</param>
    void LogInfo(string message);


    /// <summary>
    /// Logs a warning message
    /// </summary>
    /// <param name="message">The message to log.</param>
    void LogWarning(string message);


    /// <summary>
    /// Logs an error message
    /// </summary>
    /// <param name="message">The message to log.</param>
    void LogError(string message);


    /// <summary>
    /// Logs a debug message
    /// </summary>
    /// <param name="message">The message to log.</param>
    void LogDebug(string message);
}

