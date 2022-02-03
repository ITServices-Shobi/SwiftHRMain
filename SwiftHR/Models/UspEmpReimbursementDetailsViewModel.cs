using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftHR.Models
{
    public class UspEmpReimbursementDetailsViewModel
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int EmployeeNumber { get; set; }
        public string EmployeeName { get; set; }
        public int EarningsTypeFromLookUp { get; set; }
        public string EarningsType { get; set; }
        public string Date { get; set; }
        public decimal Amount { get; set; }
        public string PaymentEffectedDate { get; set; }
        public string Remarks { get; set; }
        public bool IsActive { get; set; }
        public string Status { get; set; }
    }
}
