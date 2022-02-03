using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftHR.Models
{
    public class SalaryDetails
    {
        public int ID { get; set; }
        public int HeaderID { get; set; }
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
        public bool? IsActive { get; set; }
        public string CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public int UpdatedBy { get; set; }
    }
}
