using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftHR.Models
{
    public class SALEmployeeDetails
    {
        public int EmployeeId { get; set; }
        public string EmployeeNumber { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string DateOfJoining { get; set; }
        public string DateOfBirth { get; set; }
        public string Location { get; set; }
        public bool? PFIsApplicable { get; set; }
        public string PFAccountNo { get; set; }
        public bool? AllowEPFExcessContribution { get; set; }
        public bool? AllowEPSExcessContribution { get; set; }
        public bool? ESICIsApplicable { get; set; }
        public string ESICAccountNo { get; set; }
        public string VersionNumber { get; set; }
        public int SalDetailID { get; set; }
        public decimal Basic { get; set; }
        public decimal HRA { get; set; }
        public decimal Bonus { get; set; }
        public decimal OtherAllowance { get; set; }
        public decimal Overttime { get; set; }
        public decimal ProfTax { get; set; }
        public decimal Loan { get; set; }
        public decimal AdvanceSalary { get; set; }
        public decimal EmployeeContributionPF { get; set; }
        public decimal EmployeeContributionESIC { get; set; }
        public decimal EmployerContributionPF { get; set; }
        public decimal EmployerContributionESIC { get; set; }
        public decimal MonthlyNetPay { get; set; }
        public decimal MonthlyGrossPay { get; set; }
        public decimal AnnualGrossSalary { get; set; }
        public decimal AnnualGrossCTC { get; set; }

    }
}
