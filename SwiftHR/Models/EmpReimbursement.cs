using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftHR.Models
{
    public partial class EmpReimbursement
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int EmployeeNumber { get; set; }
        public string EmployeeName { get; set; }
        public string ComponentsType { get; set; }
        public int EarningsTypeFromLookUp { get; set; }
        public DateTime Date { get; set; }
        public DateTime PaymentEffectedDate { get; set; }
        public decimal Amount { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
        public int ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }
}
