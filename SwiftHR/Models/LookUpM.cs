using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftHR.Models
{
    public partial class LookUpM
    {
        public int LookUpId { get; set; }
        public string LookUpCode { get; set; }
        public string LookUpName { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; }

    }

}
