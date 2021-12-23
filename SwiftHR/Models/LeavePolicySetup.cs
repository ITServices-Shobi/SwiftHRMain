using System;
using System.Collections.Generic;

#nullable disable

namespace SwiftHR.Models
{
    public partial class LeavePolicySetup
    {
        public int LeavePolicySetupId { get; set; }
        public int? LeaveTypeId { get; set; }
        public int? OpeningBalance { get; set; }
        public bool? IsLapsedAtEndOfTheMonth { get; set; }
        public bool? IsLapsedAtEndOfTheYear { get; set; }
        public bool? IsCarryForwordToNextMonth { get; set; }
        public bool? IsCarryForwordToNextYear { get; set; }
        public int? CarryForwordYearlyLimit { get; set; }
        public bool? IsSetLeaveForEncashment { get; set; }
        public int? LeaveEncashmentLimit { get; set; }
        public string CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
    }
}
