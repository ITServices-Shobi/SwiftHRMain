using System;
using System.Collections.Generic;

#nullable disable

namespace SwiftHR.Models
{
    public partial class SettingsDataItem
    {
        public int SettingsDataItemId { get; set; }
        public int SettingsItemTypeId { get; set; }
        public string SettingsDataItemName { get; set; }
        public string SettingsDataItemValue { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
    }
}
