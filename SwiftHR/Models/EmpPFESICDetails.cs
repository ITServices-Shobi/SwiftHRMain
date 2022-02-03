using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftHR.Models
{
    public class EmpPFESICDetails
    {
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeNumber { get; set; }
        public string EmployeeName { get; set; }
        public string BankName { get; set; }
        public int BankID { get; set; }
        public string BankBranch { get; set; }
        public int AccountTypeID { get; set; }
        public string AccountNo { get; set; }
        public string IFSCCode { get; set; }
        public string EmployeeNameAsBankRecords { get; set; }
        public string IBAN { get; set; }
        public int PaymentMethod { get; set; }
        public bool ESICIsApplicable { get; set; }
        public string ESICAccountNo { get; set; }
        public string PFAccountNo { get; set; }
        public string UAN { get; set; }
        public DateTime StartDate { get; set; }
        public bool PFIsApplicable { get; set; }
        public bool AllowEPFExcessContribution { get; set; }
        public bool AllowEPSExcessContribution { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public int UpdatedBy { get; set; }
    }
}
