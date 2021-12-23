using System;
using System.Collections.Generic;

#nullable disable

namespace SwiftHR.Models
{
    public partial class UserActionLog
    {
        public int SalogId { get; set; }
        public string Action { get; set; }
        public string CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
    }
}
