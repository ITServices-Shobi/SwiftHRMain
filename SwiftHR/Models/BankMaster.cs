using System;
using System.Collections.Generic;

#nullable disable

namespace SwiftHR.Models
{
    public partial class BankMaster
    {
        public int BankMasterDataId { get; set; }
        public string BankName { get; set; }
        public string BankCode { get; set; }
        public string Ifsccode { get; set; }
        public string CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
    }
}
