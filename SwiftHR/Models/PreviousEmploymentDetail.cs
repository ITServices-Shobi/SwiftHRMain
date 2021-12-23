using System;
using System.Collections.Generic;

#nullable disable

namespace SwiftHR.Models
{
    public partial class PreviousEmploymentDetail
    {
        public int PrevEmploymentId { get; set; }
        public int? EmployeeId { get; set; }
        public string PrevCompanyName { get; set; }
        public string PrevCompanyDesignation { get; set; }
        public string PrevFromDate { get; set; }
        public string PrevToDate { get; set; }
        public string PrevCompanyAddress { get; set; }
        public string CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
    }
}
