using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftHR.Models
{
    public partial class LoanHeader
    {
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public int EmployeeNumber { get; set; }
        public string EmployeeName { get; set; }
        public string DateOfLoan { get; set; }
        public string StartFrom { get; set; }
        public decimal LoanAmount { get; set; }
        public bool LoanCompleted { get; set; }
        public string CompletedDate { get; set; }
        public int LoanType { get; set; }
        public int NumberOfEMI { get; set; }
        public decimal MonthlyEMIAmount { get; set; }
        public decimal InterestRate { get; set; }
        public bool DemandPromissoryNote { get; set; }
        public decimal PerquisiteRate { get; set; }
        public string LoanAccountNo { get; set; }
        public decimal PrincipalBalance { get; set; }
        public decimal InterestBalance { get; set; }
        public string Remarks { get; set; }
        public bool IsActive { get; set; }
        public  int CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }

    }
}
