using System;
using System.Collections.Generic;

#nullable disable

namespace SwiftHR.Models
{
    public partial class ShiftHoursCalculationScheme
    {
        public int ShiftHoursCalculationSchemeId { get; set; }
        public string ShiftHoursCalculationSchemeName { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedDate { get; set; }
        public int? Createdby { get; set; }
    }
}
