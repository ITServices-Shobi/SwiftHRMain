using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftHR.Models
{
    public partial class LoanRevisionHeader
    {
        public int ID { get; set; }
        public int LoanHeaderID { get; set; }
        public int LoanType { get; set; }
        public decimal LoanAmount { get; set; }
        public decimal CurrentInterestRate { get; set; }
        public decimal PrincipalBalance { get; set; }
        public decimal NewLoanPeriod { get; set; }
        public decimal TopUpAmount { get; set; }
        public decimal NewInterestRate { get; set; }
        public decimal TotalInstallments { get; set; }
        public decimal NoOfInstallmentsPaid { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }

    }
}
