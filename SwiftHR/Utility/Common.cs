using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftHR.Utility
{
    public class Common
    {
        private static string date;
        private static string time;
        private static TimeZoneInfo IST_TIMEZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        public static string CurrentDate()
        {
            return (TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("dd-MMM-yyyy"));
        }

        public static string CurrentTime()
        {
            return (TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IST_TIMEZONE).ToString("HH:mm"));
        }
    }

    public class LeaveGeneralSettings
    {
        public string LeavePolicyYear { get; set; }
        public string TotalLeavesAllocated { get; set; }
    }
}
