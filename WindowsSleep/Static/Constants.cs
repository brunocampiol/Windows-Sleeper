using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsSleep.Static
{
    public static class Constants
    {
        public static readonly TimeSpan ONE_SECOND = new TimeSpan(0, 0, 1);
        public const string Ready = "Ready to start";
        public const string SecondsRemaining = " seconds remaining";
        public const string InvalidInput = " is invalid";
    }
}
