using System;
using System.Collections.Generic;

#nullable disable

namespace SwiftHR.Models
{
    public partial class EmpMasterItem
    {
        public int EmpMasterItemId { get; set; }
        public int? EmpMasterItemCategoryId { get; set; }
        public string EmpMasterItemName { get; set; }
        public string EmpMasterItemDescription { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
    }
}
