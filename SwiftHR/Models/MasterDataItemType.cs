using System;
using System.Collections.Generic;

#nullable disable

namespace SwiftHR.Models
{
    public partial class MasterDataItemType
    {
        public int ItemTypeId { get; set; }
        public string ItemTypeName { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
    }
}
