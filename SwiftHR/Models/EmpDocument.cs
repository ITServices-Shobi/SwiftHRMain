using System;
using System.Collections.Generic;

#nullable disable

namespace SwiftHR.Models
{
    public partial class EmpDocument
    {
        public int EmpDocumentId { get; set; }
        public int DocEmployeeId { get; set; }
        public string EmpDoumentName { get; set; }
        public string DocumentCategory { get; set; }
        public string DocumentFilePath { get; set; }
        public string VerificationStatus { get; set; }
        public string VerifiedDate { get; set; }
        public int? VerifiedBy { get; set; }
        public string VerificationComments { get; set; }
        public string CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
    }
}
