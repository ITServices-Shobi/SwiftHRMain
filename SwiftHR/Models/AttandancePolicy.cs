using System;
using System.Collections.Generic;

#nullable disable

namespace SwiftHR.Models
{
    public partial class AttandancePolicy
    {
        public int AttandancePolicyId { get; set; }
        public string AttandancePolicyName { get; set; }
        public string Description { get; set; }

        public bool? IsActive { get; set; }
        public string CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
    }
}
