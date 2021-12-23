using System;
using System.Collections.Generic;

#nullable disable

namespace SwiftHR.Models
{
    public partial class EmailTemplate
    {
        public int EmailTemplateId { get; set; }
        public string EmailTemplateTitle { get; set; }
        public string EmailTemplateHtml { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
    }
}
