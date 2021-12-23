using System;
using System.Collections.Generic;

#nullable disable

namespace SwiftHR.Models
{
    public partial class RoleMaster
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
    }
}
