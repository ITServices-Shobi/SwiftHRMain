using System;
using System.Collections.Generic;

#nullable disable

namespace SwiftHR.Models
{
    public partial class AttandancePolicyRulesMapping
    {
        public int AttandancePolicyRulesMappingId { get; set; }
        public int? AttandancePolicyId { get; set; }
        public int? AttandancePolicyRulesCategoryId { get; set; }
        public int? AttandancePolicyRuleId { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
    }
}
