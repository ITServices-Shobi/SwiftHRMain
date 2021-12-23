using System;
using System.Collections.Generic;

#nullable disable

namespace SwiftHR.Models
{
    public partial class Shift
    {
        public int ShiftId { get; set; }
        public string ShiftName { get; set; }
        public string ShiftCode { get; set; }
        public string HalfDayMinimumHours { get; set; }
        public string FullDayMinimumHours { get; set; }
        public int? CalculateShiftHoursBasedOnScheme { get; set; }
        public string CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
    }
}
