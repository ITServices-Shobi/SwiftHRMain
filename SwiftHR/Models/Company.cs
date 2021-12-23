using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwiftHR.Models
{
    public partial class Company
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string WebSiteName { get; set; }
        public string PhoneNo { get; set; }
        public string MobNo { get; set; }
        public string GSTNO { get; set; }
        public string PANNO { get; set; }
        public string CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public bool IsActive { get; set; }
        public string UpdateDate { get; set; }
        public int UpdateBy { get; set; }
    }
}
