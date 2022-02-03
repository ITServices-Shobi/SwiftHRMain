using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftHR.Models
{
    public class SpGetEmpReimbursementApprovedViewModel
    {
        public int Id { get; set; }
        public string EmployeeNumber { get; set; }
        public string EmployeeName { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public string Date { get; set; }
        public decimal Amount { get; set; }
        public string PaymentEffectedDate { get; set; }
        public string Remarks { get; set; }
    }
}
