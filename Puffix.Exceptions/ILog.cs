using System;

namespace Puffix.Exceptions
{
    public interface ILog
    {
        void Warning(string message);

        void Warning(string message, Exception error);

        void Warning(Exception error);

        void Error(string message);

        void Error(string message, Exception error);

        void Error(Exception error);

        void Fatal(string message);

        void Fatal(string message, Exception error);

        void Fatal(Exception error);
    }
}
