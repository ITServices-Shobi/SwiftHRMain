using SwiftHR.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SwiftHR.Utility
{
    public class LeaveSettings
    {
        SHR_SHOBIGROUP_DBContext dbContext = new SHR_SHOBIGROUP_DBContext();

        public List<LeaveTypeCategory> leaveTypeCategorySettings { get; set; }
        public List<LeaveType> leaveTypeSettings { get; set; }
        public List<LeaveTypeScheme> leaveTypeSchemeSettings { get; set; }
        public List<LeaveTypeMapping> leaveTypeMappingSettings { get; set; }
        public List<UserDetail> userDetails { get; set; }

        public LeaveSettings()
        {
            leaveTypeCategorySettings = new List<LeaveTypeCategory>();
            leaveTypeCategorySettings = dbContext.LeaveTypeCategories.Where(x => x.IsActive == true).ToList();

            leaveTypeSettings = new List<LeaveType>();
            leaveTypeSettings = dbContext.LeaveTypes.Where(x => x.IsActive == true).ToList();

            leaveTypeSchemeSettings = new List<LeaveTypeScheme>();
            leaveTypeSchemeSettings = dbContext.LeaveTypeSchemes.Where(x => x.IsActive == true).ToList();

            leaveTypeMappingSettings = new List<LeaveTypeMapping>();
            leaveTypeMappingSettings = dbContext.LeaveTypeMappings.Where(x => x.IsActive == true).ToList();

            userDetails = new List<UserDetail>();
            userDetails = dbContext.UserDetails.ToList();

        }

    }
}
