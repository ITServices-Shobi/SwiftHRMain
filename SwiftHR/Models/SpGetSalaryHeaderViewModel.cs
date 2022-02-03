using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftHR.Models
{
    public class SpGetSalaryHeaderViewModel
    {
        public int SalHeaderId { get; set; }
        public int EmployeeID { get; set; }
        public int EmployeeNumber { get; set; }
        public string EmployeeName { get; set; }
        public int EmployeeType { get; set; }
        public int PFAvailability { get; set; }
        public string DOJ { get; set; }
        public string DOB { get; set; }
        public string LastPayrollProceesedDate { get; set; }
        public int PayoutMonth { get; set; }
        public string Remarks { get; set; }
        public string EffectiveEndDate { get; set; }
        public string EffectiveStartDate { get; set; }

        //------------------- Details Fild Name -----------------

        public string SingalColName { get; set; }
        public string ColValue { get; set; }
    }
}
