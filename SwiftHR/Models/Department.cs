using System;
using System.Collections.Generic;

#nullable disable

namespace SwiftHR.Models
{
    public partial class Department
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentCode { get; set; }
        public string CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? ColorCode { get; set; }
        public int? CompanyId { get; set; }
        public bool? IsActive { get; set; }
    }
}
