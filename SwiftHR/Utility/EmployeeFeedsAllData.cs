using SwiftHR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftHR.Utility
{
    public class EmployeeFeedsAllData
    {
        public EmployeeFeed employeeFeed { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeProfilePhoto { get; set; }
        public int LikesCount { get; set; }
        public int CommentsCount { get; set; }
        public List<FeedsAllComments> feedsComments { get; set; }
    }
}
