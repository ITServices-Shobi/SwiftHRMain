using System;
using System.Collections.Generic;

#nullable disable

namespace SwiftHR.Models
{
    public partial class PageAccessSetup
    {
        public int PageAccessId { get; set; }
        public int? RoleId { get; set; }
        public int? PageModuleId { get; set; }
        public bool? IsAllow { get; set; }
        public string ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
