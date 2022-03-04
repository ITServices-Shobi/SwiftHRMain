using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftHR.Models
{
    public class SalaryHeader
    {
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeNumber { get; set; }
        public string EmployeeName { get; set; }
        public int EmployeeType { get; set; }
        public int Gender { get; set; }
        public bool PFAvailability { get; set; }
        public DateTime DOJ { get; set; }
        public DateTime DOB { get; set; }
        public DateTime LastPayrollProceesedDate { get; set; }
        public string Location { get; set; }
        public int PayoutMonth { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
        public DateTime EffectiveStartDate { get; set; }
        public DateTime EffectiveEndDate { get; set; }
        public int VersionNumber { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public int UpdatedBy { get; set; }
    }
}
