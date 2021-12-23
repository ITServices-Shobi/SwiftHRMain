using SwiftHR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftHR.Utility
{
    public class EmployeeDataCount
    {
        public List<UserDetail> userDetails { get; set; }
        public int TotalEmpCount { get; set; }
        public int ActiveEmpCount { get; set; }
        public int CurrentMonthEmpCount { get; set; }
    }
}
