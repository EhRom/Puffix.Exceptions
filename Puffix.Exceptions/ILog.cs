using System;

namespace Puffix.Exceptions;

/// <summary>
/// Logger contract.
/// </summary>
public interface ILog
{
    /// <summary>
    /// Log message (warning).
    /// </summary>
    /// <param name="message">Message.</param>
    void Warning(string message);

    /// <summary>
    /// Log exception (warning).
    /// </summary>
    /// <param name="message">Error message.</param>
    /// <param name="error">Exception.</param>
    void Warning(string message, Exception error);

    /// <summary>
    /// Log exception (warning).
    /// </summary>
    /// <param name="error">Exception to log.</param>
    void Warning(Exception error);

    /// <summary>
    /// Log error message.
    /// </summary>
    /// <param name="message">Message.</param>
    void Error(string message);

    /// <summary>
    /// Log exception.
    /// </summary>
    /// <param name="message">Error message.</param>
    /// <param name="error">Exception.</param>
    void Error(string message, Exception error);

    /// <summary>
    /// Log exception.
    /// </summary>
    /// <param name="error">Exception to log.</param>
    void Error(Exception error);

    /// <summary>
    /// Log message (fatal error).
    /// </summary>
    /// <param name="message">Message.</param>
    void Fatal(string message);

    /// <summary>
    /// Log exception (fatal error).
    /// </summary>
    /// <param name="message">Error message.</param>
    /// <param name="error">Exception.</param>
    void Fatal(string message, Exception error);

    /// <summary>
    /// Log exception (fatal error).
    /// </summary>
    /// <param name="error">Exception to log.</param>
    void Fatal(Exception error);
}
