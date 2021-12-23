using System;
using System.Collections.Generic;

#nullable disable

namespace SwiftHR.Models
{
    public class EmpAddress
    {
        public int EmpAddressId { get; set; }
        public int EmployeeId { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Pin { get; set; }
        public bool IsPermanentAddress { get; set; }
        public string CreatedDate { get; set; }
        public int? CreatedBy { get; set; }

    }
}
