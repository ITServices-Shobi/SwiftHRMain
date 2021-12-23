using System;
using System.Collections.Generic;

#nullable disable

namespace SwiftHR.Models
{
    public partial class EmpGeneralSetting
    {
        public int EmpGeneralSettingId { get; set; }
        public int? ProbationPeriod { get; set; }
        public int? EmployeeRetirementAgeInYears { get; set; }
    }
}
