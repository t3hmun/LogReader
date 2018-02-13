using System;
using Xunit;

namespace LogReader.Tests
{
    public class LogReaderTests
    {
        [Theory]
        [InlineData("2018-07-06 09:02:17.148 +10:00 [INF]", true)]
        [InlineData("2018-07-06 09:02:17.148 +10:00 [potato]", false)]
        public void CanMatchHeader(string headerLine, bool isMatch)
        {
            Assert.True(LogReader.DefaultIsLogLine(headerLine) == isMatch);
        }
    }
}