using System;
using System.Collections.Generic;

#nullable disable

namespace SwiftHR.Models
{
    public partial class AttandancePolicyRule
    {
        public int AttandancePolicyRuleId { get; set; }
        public int? AttandancePolicyRulesCategoryId { get; set; }
        public string AttandancePolicyRuleName { get; set; }
        public string AttandancePolicyRule1 { get; set; }
        public string Description { get; set; }
        public string Example { get; set; }
        public string MarkStatusFor { get; set; }
        public bool? SendNotification { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
    }
}
