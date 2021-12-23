using System;
using System.Collections.Generic;

#nullable disable

namespace SwiftHR.Models
{
    public partial class EmpInfoConfiguration
    {
        public int EmpInfoConfigurationId { get; set; }
        public int? EmpInfoSectionId { get; set; }
        public string EmpInfoConfigItem { get; set; }
        public bool? SetToDisplay { get; set; }
        public bool? SetToMandatory { get; set; }
        public bool? SetForAttachmentsDisplay { get; set; }
        public bool? SetForAttachmentsMandatory { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
    }
}
