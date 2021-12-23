using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftHR.Models
{
    public partial class TimeRecordingSheet
    {
        public int RecTimeSheetId { get; set; }
        public int EmployeeId { get; set; }
        public string Year { get; set; }
        public string month { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
    }
}
