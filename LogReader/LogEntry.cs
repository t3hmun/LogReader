namespace LogReader
{
    using JetBrains.Annotations;

    /// <summary>
    ///     An entry from a log file with its content.
    /// </summary>
    public class LogEntry
    {
        public LogEntry(string headLine, string content, string message)
        {
            // TODO: Add loglevel enum.
            HeadLine = headLine;
            Content = content;
            Message = message;
        }

        /// <summary>
        ///     The entire first lines of the log message.
        /// </summary>
        public string HeadLine { get; }

        /// <summary>
        ///     The content, everything after the first line of the log message.
        /// </summary>
        [CanBeNull]
        public string Content { get; }

        /// <summary>
        ///     The message after the timestamp and log level on then first line.
        /// </summary>
        public string Message { get; }
    }
}