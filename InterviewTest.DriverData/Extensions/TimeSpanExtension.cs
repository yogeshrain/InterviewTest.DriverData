using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTest.DriverData.Extensions
{
    public static class TimeSpanExtension
    {
        public static bool IsInRange(this TimeSpan current,TimeSpan endPeriod,TimeSpan start, TimeSpan end)
        {
            return (current <= start && endPeriod > start) ||
                (current >= start && current < end);
        }
    }
}
