using System;
using System.Collections.Generic;

#nullable disable

namespace SwiftHR.Models
{
    public partial class FeedsCommentsAndLike
    {
        public int FeedsClid { get; set; }
        public int? FeedsId { get; set; }
        public int? EmployeeId { get; set; }
        public string Comments { get; set; }
        public bool? IsComment { get; set; }
        public bool? IsLike { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedTime { get; set; }
    }
}
