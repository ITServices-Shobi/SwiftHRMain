using System;
using System.Collections.Generic;

#nullable disable

namespace SwiftHR.Models
{
    public partial class PurchaseModuleScheme
    {
        public int PurchaseModuleSchemeId { get; set; }
        public string PurchaseModuleName { get; set; }
        public string PurchaseModuleCode { get; set; }
        public string Description { get; set; }
        public string CreatedDate { get; set; }
        public int? Createdby { get; set; }
    }
}
