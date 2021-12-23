using System;
using System.Collections.Generic;

#nullable disable

namespace SwiftHR.Models
{
    public partial class FeedsGroup
    {
        public int FeedsGroupId { get; set; }
        public string FeedsGroupName { get; set; }
        public string FeedsGroupDescription { get; set; }
        public string CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
    }
}
