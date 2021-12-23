using System;
using System.Collections.Generic;

#nullable disable

namespace SwiftHR.Models
{
    public partial class EmpSettingsCategoryValue
    {
        public int EmpSettingsCategoryValueId { get; set; }
        public int? EmpSettingsCategoryId { get; set; }
        public string EmpSettingsCategoryValue1 { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
    }
}
