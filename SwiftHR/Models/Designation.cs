using System;
using System.Collections.Generic;

#nullable disable

namespace SwiftHR.Models
{
    public partial class Designation
    {
        public int DesignationId { get; set; }
        public string DesignationName { get; set; }
        public string DesignationCode { get; set; }
        public string CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
    }
}
