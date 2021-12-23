using System;
using System.Collections.Generic;

#nullable disable

namespace SwiftHR.Models
{
    public partial class EmpMasterItemCategory
    {
        public int EmpMasterItemCategoryId { get; set; }
        public string EmpMasterItemCategoryName { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
    }
}
