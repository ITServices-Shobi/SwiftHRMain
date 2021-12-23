using System;
using System.Collections.Generic;

#nullable disable

namespace SwiftHR.Models
{
    public partial class LeaveType
    {
        public int LeaveTypeId { get; set; }
        public int? LeaveTypeCategoryId { get; set; }
        public string LeaveTypeName { get; set; }
        public string Code { get; set; }
        public string SortOrder { get; set; }
        public string Description { get; set; }
        public bool? IsEmpAllowedToApply { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
    }
}
