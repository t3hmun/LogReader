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
        /// <summary>
        /// A matcher for the defualt format for the Serilog File sink.
        /// 2018-07-06 09:02:17.148 +10:00 [INF]
        /// </summary>
        private static readonly Regex DefaultLineMatcherRegex =
            new Regex(@"^(\d{4}-\d\d-\d\d \d\d:\d\d:\d\d\.\d+ ((\-|\+)\d\d:\d\d)? \[(VRB|DBG|INF|WRN|ERR|FTL)\])");

        
        public static IEnumerable<LogEntry> ReadLog(IEnumerable<string> logLines, Func<string, bool> logLineMatcher)
        {
            string headerLine = null;

            var start = logLines.SkipWhile(line =>
            {
                var isMatch = logLineMatcher(line);
                if (!isMatch) return true;
                headerLine = line;
                return false;
            });
            
            var content = new List<string>();
            foreach (var line in start)
            {
                var isMatch = logLineMatcher(line);
                if (isMatch)
                {
                    var entry = new LogEntry(headerLine, content, "");
                    yield return entry;
                    headerLine = line;
                    content = new List<string>();
                }
                else
                {
                    content.Add(line);
                }
            }
           
            // This happens if the collection is empty.
            if(headerLine == null) yield break;
            
            yield return new LogEntry(headerLine, content, "");
        }
        
       
        public static bool DefaultLogHeadingMatcher(string line)
        {
            return DefaultLineMatcherRegex.IsMatch(line);
        }
    }
}