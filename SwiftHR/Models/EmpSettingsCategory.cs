using System;
using System.Collections.Generic;

#nullable disable

namespace SwiftHR.Models
{
    public partial class EmpSettingsCategory
    {
        public int EmpSettingsCategoryId { get; set; }
        public string EmpSettingsCategoryName { get; set; }
        public string EmpSettingsCategoryDescription { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
    }
}
