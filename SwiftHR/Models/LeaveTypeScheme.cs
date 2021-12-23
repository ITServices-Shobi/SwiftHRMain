using System;
using System.Collections.Generic;

#nullable disable

namespace SwiftHR.Models
{
    public partial class LeaveTypeScheme
    {
        public int LeaveTypeSchemeId { get; set; }
        public string LeaveTypeScemeName { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
    }
}
