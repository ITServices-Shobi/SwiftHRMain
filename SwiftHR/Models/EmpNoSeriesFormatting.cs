using System;
using System.Collections.Generic;

#nullable disable

namespace SwiftHR.Models
{
    public partial class EmpNoSeriesFormatting
    {
        public int EmployeeNoSeriesId { get; set; }
        public string EmpSeriesName { get; set; }
        public string SerialNo { get; set; }
        public string Format { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; }
        public string MappingWithEmployeeStatus { get; set; }
    }
}
