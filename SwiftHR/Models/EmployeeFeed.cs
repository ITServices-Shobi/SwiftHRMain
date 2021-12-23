using System;
using System.Collections.Generic;

#nullable disable

namespace SwiftHR.Models
{
    public partial class EmployeeFeed
    {
        public int FeedsId { get; set; }
        public int? EmployeeId { get; set; }
        public int? FeedsGroupId { get; set; }
        public string FeedsDescription { get; set; }
        public string FeedsFileName { get; set; }
        public string VisibilityDate { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedTime { get; set; }
    }
}
