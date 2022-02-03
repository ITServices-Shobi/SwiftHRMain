using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftHR.Models
{
    public partial class LoanDetails
    {
        public int ID { get; set; }
        public int LoanHeaderID { get; set; }
        public int TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }
        public decimal ToInterest { get; set; }
        public decimal ToPrincipal { get; set; }
        public decimal ActualInterest { get; set; }
        public decimal ActualPrincipal { get; set; }
        public decimal PerkValue { get; set; }
        public decimal PerkAmount { get; set; }
        public decimal PerkRate { get; set; }
        public string Remarks { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }

    }
}
