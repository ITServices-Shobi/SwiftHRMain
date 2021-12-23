using SwiftHR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftHR.Utility
{
    public class DashBoard
    {
        SHR_SHOBIGROUP_DBContext _context = new SHR_SHOBIGROUP_DBContext();
        public List<Employee> empMasterDataItems { get; set; }

        public List<LeaveApplyDetail> leaveApplyListAll { get; set; }

        public List<Department> departmentListAll { get; set; }

        public List<EmpOnboardingDetail> empOnboardingDetails { get; set; }


        public String GetEmployeeDepartmentColorByDeptNumber(string deptName)
        {
            if (deptName != null && deptName != "")
            {
                foreach (var empd in _context.Departments.Where(x => x.DepartmentName == deptName).ToList())
                {

                    return empd.ColorCode.ToString();
                }

            }
            return "1";
        }
               
        public string DepartmentTruncate(string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value.PadRight(maxLength,' ');
            }
            else
            {
                value = value.PadLeft(maxLength);
                return value;
                //return value.Length <= maxLength ? value.PadLeft(maxLength) : value.Substring(0, maxLength);
                //return value.Length <= maxLength ? value.PadRight(maxLength,' ').Substring(0, maxLength) : value.Substring(0, maxLength);
            }
            
        }

        public String GetDepartmentWiseEmployeePercentage(float deptWiseEmpCount, float totalEmployees)
        {
            if (deptWiseEmpCount > 0 && totalEmployees > 0)
            {
                float result=(deptWiseEmpCount / totalEmployees) * 100;
                
                return Math.Round(result,0).ToString();
            }
            return "0";
        }
    }
}
