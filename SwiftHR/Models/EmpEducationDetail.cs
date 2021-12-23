using System;
using System.Collections.Generic;

#nullable disable

namespace SwiftHR.Models
{
    public partial class EmpEducationDetail
    {
        public int EmpEducationId { get; set; }
        public int EduEmployeeId { get; set; }
        public string Degree { get; set; }
        public string Program { get; set; }
        public string NameOfInstitute { get; set; }
        public string PassingYear { get; set; }
        public string Percentage { get; set; }
        public string DocumentFileName { get; set; }
        public string CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public string VerificationStatus { get; set; }
        public string VerifiedDate { get; set; }
        public int? VerifiedBy { get; set; }
        public string VerificationComments { get; set; }
    }
}
