using SwiftHR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftHR.Utility
{
    public class EmployeeUserDetails
    {
        public UserDetail userDetails { get; set; }
        public Employee empDetails { get; set; }

        public bool addEmployeeAccess { get; set; }
        public bool employeeListAccess { get; set; }
        public bool generalSettingsAccess { get; set; }
        public bool leaveTypesAccess { get; set; }
        public bool leaveRulesAccess { get; set; }
        public bool leavePolicySetupAccess { get; set; }
        public bool notificationsAccess { get; set; }
        public bool leaveReasonsAccess { get; set; }
        public bool leaveReportsAccess { get; set; }
        public bool shiftsAccess { get; set; }
        public bool attendancePolicySetupAccess { get; set; }
        public bool attendanceSchemeAccess { get; set; }
        public bool showLeavesAccess { get; set; }
        public bool applyLeaveAccess { get; set; }
        public bool showAttendanceAccess { get; set; }
        public bool helpDeskSetupAccess { get; set; }
        public bool helpDeskAnalysisAccess { get; set; }
        public bool reportsMappingAccess { get; set; }
        public bool dashboardAccess { get; set; }
        public bool eSSDashboardAccess { get; set; }
        public string userRoleName { get; set; }

    }
}
