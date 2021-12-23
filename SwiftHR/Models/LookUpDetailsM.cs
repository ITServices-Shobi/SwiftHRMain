using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftHR.Models
{
    public partial class LookUpDetailsM
    {
        public int LookUpDetailsId { get; set; }
        public int LookUpId { get; set; }
        //public string LookUpName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; }
    }
}
