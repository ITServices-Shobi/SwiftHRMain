using System;
using System.Collections.Generic;

#nullable disable

namespace SwiftHR.Models
{
    public partial class MasterDataItem
    {
        public int MasterDataItemId { get; set; }
        public int ItemTypeId { get; set; }
        public string MasterDataItemValue { get; set; }
        public string ItemDescription { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
    }
}
