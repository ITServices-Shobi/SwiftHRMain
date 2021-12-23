using System;
using System.Collections.Generic;

#nullable disable

namespace SwiftHR.Models
{
    public partial class SettingsDataItemType
    {
        public int SettingsItemTypeId { get; set; }
        public string SettingsItemTypeName { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
    }
}
