using System;
using System.Collections.Generic;
using System.Text;
using WindowsSleep.Models;

namespace WindowsSleep.Static
{
    public static class TimeTypeHelper
    {
        public static TimeSpan GetTimeSpan(string time, TimeType type)
        {
            TimeSpan timeSpan;

            int timeInteger = Convert.ToInt32(time);

            switch (type)
            {
                case TimeType.Seconds:

                    timeSpan = new TimeSpan(0, 0, timeInteger);
                    break;
                case TimeType.Minutes:
                    timeSpan = new TimeSpan(0, timeInteger, 0);
                    break;
                case TimeType.Hours:
                    timeSpan = new TimeSpan(timeInteger, 0, 0);
                    break;
                case TimeType.Days:
                    timeSpan = new TimeSpan(timeInteger, 0, 0, 0, 0);
                    break;
                default:
                    timeSpan = new TimeSpan();
                    break;
            }

            return timeSpan;
        }

        public static TimeType GetTimeType(string timeType)
        {
            if (timeType.Equals(TimeType.Seconds.ToString(), StringComparison.InvariantCultureIgnoreCase)) return TimeType.Seconds;
            if (timeType.Equals(TimeType.Minutes.ToString(), StringComparison.InvariantCultureIgnoreCase)) return TimeType.Minutes;
            if (timeType.Equals(TimeType.Hours.ToString(), StringComparison.InvariantCultureIgnoreCase)) return TimeType.Hours;
            if (timeType.Equals(TimeType.Days.ToString(), StringComparison.InvariantCultureIgnoreCase)) return TimeType.Days;

            // TODO better time type
            return TimeType.Seconds;
        }
    }
}
