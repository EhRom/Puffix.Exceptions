using System;

namespace Puffix.Exceptions.Basic;

/// <summary>
/// Empty logger. This class is a sample logger class, which mocks a logger.
/// </summary>
public class EmptyLog : ILog
{
    /// <summary>
    /// Logger name.
    /// </summary>
    private readonly string loggerName;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="type">Type in which the logger is used. The type is used for the logger name.</param>
    private EmptyLog(Type type)
        : this(type?.FullName)
    { }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="loggerName">Name of the logger.</param>
    private EmptyLog(string loggerName)
    {
        this.loggerName = loggerName;
    }

    /// <summary>
    /// Get an instance of the logger.
    /// </summary>
    /// <param name="type">Type in which the logger is used. The type is used for the logger name.</param>
    /// <returns>Logger.</returns>
    public static EmptyLog GetLogger(Type type)
    {
        return new EmptyLog(type);
    }

    /// <summary>
    /// Get an instance of the logger.
    /// </summary>
    /// <param name="loggerName">Name of the logger.</param>
    /// <returns>Logger.</returns>
    public static EmptyLog GetLogger(string loggerName)
    {
        return new EmptyLog(loggerName);
    }

    /// <summary>
    /// Log message (warning).
    /// </summary>
    /// <param name="message">Message.</param>
    public void Warning(string message)
    {
        // No action. Implement the log logic here.
    }

    /// <summary>
    /// Log exception (warning).
    /// </summary>
    /// <param name="message">Error message.</param>
    /// <param name="error">Exception.</param>
    public void Warning(string message, Exception error)
    {
        // No action. Implement the log logic here.
    }

    /// <summary>
    /// Log exception (warning).
    /// </summary>
    /// <param name="error">Exception to log.</param>
    public void Warning(Exception error)
    {
        // No action. Implement the log logic here.
    }

    /// <summary>
    /// Log error message.
    /// </summary>
    /// <param name="message">Message.</param>
    public void Error(string message)
    {
        // No action. Implement the log logic here.
    }

    /// <summary>
    /// Log exception.
    /// </summary>
    /// <param name="message">Error message.</param>
    /// <param name="error">Exception.</param>
    public void Error(string message, Exception error)
    {
        // No action. Implement the log logic here.
    }

    /// <summary>
    /// Log exception.
    /// </summary>
    /// <param name="error">Exception to log.</param>
    public void Error(Exception error)
    {
        // No action. Implement the log logic here.
    }

    /// <summary>
    /// Log message (fatal error).
    /// </summary>
    /// <param name="message">Message.</param>
    public void Fatal(string message)
    {
        // No action. Implement the log logic here.
    }

    /// <summary>
    /// Log exception (fatal error).
    /// </summary>
    /// <param name="message">Error message.</param>
    /// <param name="error">Exception.</param>
    public void Fatal(string message, Exception error)
    {
        // No action. Implement the log logic here.
    }

    /// <summary>
    /// Log exception (fatal error).
    /// </summary>
    /// <param name="error">Exception to log.</param>
    public void Fatal(Exception error)
    {
        // No action. Implement the log logic here.
    }
}
