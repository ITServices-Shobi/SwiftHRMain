﻿using System;
using System.Collections.Generic;

#nullable disable

namespace SwiftHR.Models
{
    public partial class CurrancyMaster
    {
        public int CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public string Code { get; set; }
        public string CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
    }
}
