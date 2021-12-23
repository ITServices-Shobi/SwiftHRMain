using System;
using System.Collections.Generic;

#nullable disable

namespace SwiftHR.Models
{
    public partial class AuthorizedSignatory
    {
        public int AuthorizedSignatoryId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Designation { get; set; }
        public bool? IsActive { get; set; }
        public string SignatureImagePath { get; set; }
        public string SectionName { get; set; }
        public string CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
    }
}
