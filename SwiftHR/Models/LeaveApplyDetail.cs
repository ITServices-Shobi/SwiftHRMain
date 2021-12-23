using System;
using System.Collections.Generic;

#nullable disable

namespace SwiftHR.Models
{
    public partial class LeaveApplyDetail
    {
        public int EmpLeaveId { get; set; }
        public int? EmployeeId { get; set; }
        public string LeaveType { get; set; }
        public string LeaveReason { get; set; }
        public string LeaveFromDate { get; set; }
        public string LeaveToDate { get; set; }
        public string LeaveAppliedOn { get; set; }
        public int LeaveStatus { get; set; }
        public int? ReportingManagerUserId { get; set; }
        public string ReportingManagerName { get; set; }
        public string LeaveStatusChangeDate { get; set; }
        public int? LeaveStatusChangedBy { get; set; }
        public string LeaveRejectReason { get; set; }
    }
}
