using System;
using System.Collections.Generic;

#nullable disable

namespace SwiftHR.Models
{
    public partial class AttandancePolicyRulesCategory
    {
        public int AttandancePolicyRulesCategoryId { get; set; }
        public int AttandancePolicyId { get; set; }
        public string AttandancePolicyRulesCategoryName { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
    }
}
