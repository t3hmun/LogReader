using System;
using Xunit;

namespace LogReader.Tests
{
    public class LogReaderTests
    {
        [Theory]
        [InlineData("2018-07-06 09:02:17.148 +10:00 [VRB]", true, "verbose")]
        [InlineData("2018-07-06 09:02:17.148 +10:00 [INF]", true, "information")]
        [InlineData("2018-07-06 09:02:17.148 +10:00 [WRN]", true, "warning")]
        [InlineData("2018-07-06 09:02:17.148 +10:00 [ERR]", true, "error")]
        [InlineData("2018-07-06 09:02:17.148 +10:00 [FTL]", true, "fatal")]
        [InlineData("2018-07-06 09:02:17.148 -10:00 [INF]", true, "negative time zone")]
        [InlineData("2018-07-06 09:02:17.148 +01:00 [INF]", true, "positive time zone")]
        [InlineData("2018-07-06 09:02:17.148 [INF]", true, "no time zone")]
        [InlineData("2018-07-06 09:02:17.148 +10:00 [potato]", false, "invalid error level")]
        [InlineData("2018-07-06 09:02:17.148 +10:00 [AAA]", false, "invalid error level")]
        [InlineData("2018-07-06 09:02:17.148 +10:00 []", false, "invalid error level")]
        [InlineData("2018-07-06 09:02:17.148 +10:00 VRB", false, "invalid error level")]
        public void CanMatchHeader(string headerLine, bool isMatch, string testCaseInfo)
        {
            Assert.True(LogReader.DefaultLogHeadingMatcher(headerLine) == isMatch);
        }
    }
}