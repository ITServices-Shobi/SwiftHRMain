using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftHR.Models
{
    public class EmpArrearDetails
    {
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeNumber { get; set; }
        public string EmployeeName { get; set; }
        public int PayrollMonth { get; set; }
        public DateTime EffectiveDateFrom { get; set; }
        public decimal Amount { get; set; }
        public string Remarks { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedDate { get; set; }
        public int? CreatedBy { get; set; }

    }
}
