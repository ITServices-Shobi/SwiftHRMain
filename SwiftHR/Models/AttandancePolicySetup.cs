using System;
using System.Collections.Generic;

#nullable disable

namespace SwiftHR.Models
{
    public partial class AttandancePolicySetup
    {
        public int AttandancePolicySetupId { get; set; }
        public string SchemeName { get; set; }
        public int? ShiftRotationPolicyId { get; set; }
        public int? WeekendPolicyId { get; set; }
        public int? SwipeCapturingMethodId { get; set; }
        public int? ActualHourComputationMethodId { get; set; }
        public int? AttendancePolicyId { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
    }
}
