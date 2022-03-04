using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftHR.Models
{
    public class USpSendMailSalaryMonthlyStatementViewModel
    {
        public int EmployeeId { get; set; }
        public int ID { get; set; }
        public string EmployeeName { get; set; }
        public string DOJ { get; set; }
        public int DaysInMonth { get; set; }
        public decimal LOP { get; set; }
        public decimal TotalWorkingDays { get; set; }
        public string Month { get; set; }
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
        public decimal TotalDeduction { get; set; }
        public decimal NetPay { get; set; }

    }
}
