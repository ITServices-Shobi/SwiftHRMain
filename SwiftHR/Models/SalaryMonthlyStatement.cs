using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftHR.Models
{
    public class SalaryMonthlyStatement
    {
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeNumber { get; set; }
        public string EmployeeName { get; set; }
        public string DOJ { get; set; }
        public string DaysInMonth { get; set; }
        public string LOPDays { get; set; }
        public string TotalWorkingDays { get; set; }
        public string PayoutMonth { get; set; }
        public int PayoutMonthInNo { get; set; }
        public int PayoutYR { get; set; }
        public int VersionNumber { get; set; }
        public decimal Basic { get; set; }
        public decimal HRA { get; set; }
        public decimal Bonus { get; set; }
        public decimal OtherAllowance { get; set; }
        public decimal Overttime { get; set; }
        public decimal ProfTax { get; set; }
        public decimal Arrears { get; set; }
        public decimal Reimbursement { get; set; }
        public decimal Loan { get; set; }
        public decimal AdvanceSalary { get; set; }
        public decimal MonthlyPF { get; set; }
        public decimal MonthlyESIC { get; set; }
        public decimal MonthlyNetPay { get; set; }
        public decimal MonthlyGrossPay { get; set; }
        public decimal TotalDeduction { get; set; }
        public decimal NetPay { get; set; }
        public bool? IsActive { get; set; }
        public string MailSendStatus { get; set; }
    }
}
