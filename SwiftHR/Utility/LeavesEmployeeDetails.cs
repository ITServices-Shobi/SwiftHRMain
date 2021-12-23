using SwiftHR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftHR.Utility
{
    public class LeavesEmployeeDetails
    {
        SHR_SHOBIGROUP_DBContext _context = new SHR_SHOBIGROUP_DBContext();
        public List<LeaveApplyDetail> leaveApplyListAll { get; set; }
        
        public Employee empDetails { get; set; }

        public bool addEmployeeAccess { get; set; }

        public LeavesEmployeeDetails(string empId)
        {
            if (empId != null && empId != "")
            {
                Employee empData = new Employee();
                empData = _context.Employees.Where(x => x.EmployeeId == Convert.ToInt32(empId)).ToList().SingleOrDefault();

                empDetails = empData;

                leaveApplyListAll = _context.LeaveApplyDetails.ToList();

            }

        }
    }
}
