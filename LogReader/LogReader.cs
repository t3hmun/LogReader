namespace LogReader
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    /// <summary>
    ///     Bunch of methods for reading logs.
    /// </summary>
    public static class LogReader
    {
        private static readonly Regex _defaultLineMatcher =
            new Regex(@"^(\d{4}-\d\d-\d\d \d\d:\d\d:\d\d\.\d+ ((\-|\+)\d\d:\d\d)? \[(VRB|DBG|INF|WRN|ERR|FTL)\])");

        public static IEnumerable<LogEntry> ReadLog(string[] logLines, Func<string, bool> logLineMatcher,
            Action levelMatcher)
        {
            // Default format
            //2018-07-06 09:02:17.148 +10:00 [INF]

            var headLines = logLines.Select((line, i) => (Index: i, Line: line))
                .Where(item => logLineMatcher(item.Line))
                .OrderBy(item => item.Index)
                .ToArray();

            for (var i = 0; i < headLines.Length; i++)
            {
                var headLine = headLines[i];
                int contentEndIndex;

                if (i + 1 < headLines.Length)
                {
                    var nextHeadLine = headLines[i + 1];
                    contentEndIndex = nextHeadLine.Index - 1;
                }
                else
                {
                    contentEndIndex = logLines.Length - 1;
                }

                string content;
                if (contentEndIndex == i) content = null;
                else
                    content = string.Join("\n", logLines.SubList(headLine.Index + 1, contentEndIndex));

                // TODO: extract message and loglevel form the headLine.
                var entry = new LogEntry(headLine.Line, content, "");
                yield return entry;
            }
        }


        public static bool DefaultIsLogLine(string line)
        {
            return _defaultLineMatcher.IsMatch(line);
        }
    }
}