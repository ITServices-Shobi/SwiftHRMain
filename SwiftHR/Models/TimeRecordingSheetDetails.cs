using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftHR.Models
{
    public partial class TimeRecordingSheetDetails
    {
        public int RecTimeSheetDetailsId { get; set; }
        
        public int EmpId { get; set; }
        public DateTime Date { get; set; }
        public string Day { get; set; }
        public int Month { get; set; }
        public int DayNo { get; set; }
        public string EmpIn { get; set; }
        public string EmpOut { get; set; }
        public string Total { get; set; }
        public string EmpBreak { get; set; }
        public string Net { get; set; }
        public string Presence { get; set; }
        public string AttandanceStatus { get; set; }
        public bool IsSubmitData { get; set; }
        public bool IsActive { get; set; }
        public bool IsApprove { get; set; }
    }


    public partial class TimeRecordingSheetDetailsModel
    {
        public int RecTimeSheetDetailsId { get; set; }

        public int EmpId { get; set; }
        public string Date { get; set; }
        public string Day { get; set; }
        public int Month { get; set; }
        public int DayNo { get; set; }
        public string EmpIn { get; set; }
        public string EmpOut { get; set; }
        public string Total { get; set; }
        public string EmpBreak { get; set; }
        public string Net { get; set; }
        public string Presence { get; set; }
        public string AttandanceStatus { get; set; }
        public bool IsSubmitData { get; set; }
        public bool IsActive { get; set; }
        public bool IsApprove { get; set; }
    }
}
