using System;
using System.Collections.Generic;

#nullable disable

namespace SwiftHR.Models
{
    public partial class EmpBankDetail
    {
        public int EmpBankDetailsId { get; set; }
        public int BankEmployeeId { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string BankAccountNumber { get; set; }
        public string Ifsc { get; set; }
        public string AccountType { get; set; }
        public string DdpayableAt { get; set; }
        public string PaymentType { get; set; }
        public string NameAsPerBankRecords { get; set; }
        public string DocumentFileName { get; set; }
        public string VerificationStatus { get; set; }
        public string VerifiedDate { get; set; }
        public int? VerifiedBy { get; set; }
        public string VerificationComments { get; set; }
        public string CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
    }
}
