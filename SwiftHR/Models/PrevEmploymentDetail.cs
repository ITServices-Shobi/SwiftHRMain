using System;
using System.Collections.Generic;

#nullable disable

namespace SwiftHR.Models
{
    public partial class PrevEmploymentDetail
    {
        public int PrevEmploymentDetailsId { get; set; }
        public int PrevEmployeeId { get; set; }
        public int PrevEmploymentOrder { get; set; }
        public string PrevEmploymentName { get; set; }
        public string PrevCompanyAddress { get; set; }
        public string Designation { get; set; }
        public string JoinedDate { get; set; }
        public string LeavingDate { get; set; }
        public string LeavingReason { get; set; }
        public string ContactPerson1 { get; set; }
        public string ContactPerson1No { get; set; }
        public string ContactPerson2 { get; set; }
        public string ContactPerson2No { get; set; }
        public string ContactPerson3 { get; set; }
        public string ContactPerson3No { get; set; }
        public string VerificationStatus { get; set; }
        public string VerifiedDate { get; set; }
        public int? VerifiedBy { get; set; }
        public string VerificationComments { get; set; }
        public string CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
    }
}
