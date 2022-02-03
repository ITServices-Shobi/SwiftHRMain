using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftHR.Models
{
    public class UspLoanDetailsViewModel
    {
        public int ID { get; set; }
        public string Date { get; set; }
        public int LoanType { get; set; }
        public decimal Amount { get; set; }
        public decimal ToPrincipale { get; set; }
        public decimal ToInterest { get; set; }
        public decimal ActualPrincipal { get; set; }
        public decimal ActualInterest { get; set; }
        public string Remarks { get; set; }
        public decimal PerkValue { get; set; }
        public decimal PerkAmt { get; set; }
        public decimal PerkRate { get; set; }

    }
}

