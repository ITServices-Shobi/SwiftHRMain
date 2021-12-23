using SwiftHR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftHR.Utility
{
    public class LeavesAllDetails
    {
        SHR_SHOBIGROUP_DBContext dbContext = new SHR_SHOBIGROUP_DBContext();

        public List<Employee> empMasterDataItems { get; set; }

        public List<LeaveApplyDetail> leaveApplyListAll { get; set; }

        //public LeaveApplyDetail empLeaveApplyDetails { get; set; }


        //Constructor
        public LeavesAllDetails()
        {
           
        }
        public LeavesAllDetails(string empId)
        {
            leaveApplyListAll = new List<LeaveApplyDetail>();
            leaveApplyListAll = dbContext.LeaveApplyDetails.Where(x => x.EmployeeId == Convert.ToInt32(empId)).ToList();
            
        }

        public int ChangeLeavesStatus(string empId, string leaveId, string leaveStatus)
        {

            int success = 0;
            if (leaveApplyListAll.Count > 0)
            {
                foreach (var empDoc in leaveApplyListAll.Where(x => x.EmpLeaveId == Convert.ToInt32(leaveId) && x.EmployeeId == Convert.ToInt32(empId)).ToList())
                {
                    empDoc.LeaveStatus = Convert.ToInt32(leaveStatus);
                }
            }

            success = this.dbContext.SaveChanges();
            return success;

        }

    }
}