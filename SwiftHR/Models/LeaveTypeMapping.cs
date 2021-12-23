using System;
using System.Collections.Generic;

#nullable disable

namespace SwiftHR.Models
{
    public partial class LeaveTypeMapping
    {
        public int LeaveTypeMappingId { get; set; }
        public int? LeaveTypeId { get; set; }
        public int? LeaveTypeSchemeId { get; set; }
        public string LeaveAllotNoOfDaysPerMonth { get; set; }
        public string LeaveAllotTotalNoDaysInYear { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
    }
}
