using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftHR.Models
{
    public partial class LoanRevisionDetails
    {
        public int ID { get; set; }
        public int LoanRevisionID { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public DateTime EffectiveTill { get; set; }
        public decimal OpeningBalance { get; set; }
        public decimal TopUpAmount { get; set; }
        public decimal LoanAmount { get; set; }
        public decimal OverallLoanAmount { get; set; }
        public decimal InterestRate { get; set; }
        public string RemainingPeriod { get; set; }
        public string Remarks { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
    }
}
