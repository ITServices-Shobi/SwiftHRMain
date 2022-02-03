using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftHR.Models
{
    public partial class CreateEmpPayRollMonth
    {
        public int Id { get; set; }
        public int PayRollMonth { get; set; }
        public DateTime FromPayRollPeriod { get; set; }
        public DateTime ToPayRollPeriod { get; set; }
        public bool Status { get; set; }
        public bool IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }
}
