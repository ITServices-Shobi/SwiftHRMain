using System;
using System.Collections.Generic;

#nullable disable

namespace SwiftHR.Models
{
    public partial class ShiftSession
    {
        public int ShiftSessionId { get; set; }
        public string ShiftSessionName { get; set; }
        public string Description { get; set; }
        public int? ShiftId { get; set; }
        public string InTime { get; set; }
        public string OutTime { get; set; }
        public string GraceInTime { get; set; }
        public string GraceOutTime { get; set; }
        public string InMarginTime { get; set; }
        public string OutMarginTime { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
    }
}
