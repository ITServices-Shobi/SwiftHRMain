using System;
using System.Collections.Generic;

#nullable disable

namespace SwiftHR.Models
{
    public partial class EmpLeavingReason
    {
        public int EmpLeavingReasonId { get; set; }
        public string EmpLeavingReason1 { get; set; }
        public string Description { get; set; }
        public string Pfcode { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedDate { get; set; }
        public int? Createdby { get; set; }
    }
}
