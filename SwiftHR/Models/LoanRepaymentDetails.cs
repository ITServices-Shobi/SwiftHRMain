using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftHR.Models
{
    public partial class LoanRepaymentDetails
    {
        public int ID { get; set; }
        public int LoanHeaderID { get; set; }
        public DateTime RepaymentDate { get; set; }
        public decimal ToInterest { get; set; }
        public decimal ToPrincipal { get; set; }
        public decimal Amount { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Remarks { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }

    }
}
