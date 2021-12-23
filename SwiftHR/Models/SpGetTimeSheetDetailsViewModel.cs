using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftHR.Models
{
    public class SpGetTimeSheetDetailsViewModel
    {
        public int year { get; set; }
        public int Month { get; set; }
        public int EmpId { get; set; }
        public string EmpCode { get; set; }
        public string EmpName { get; set; } 
        public string InFrom { get; set; }
        public string OutTill { get; set; }
        public string Total { get; set; }
        public string Net { get; set; }

    }
}
