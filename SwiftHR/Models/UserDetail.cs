using System;
using System.Collections.Generic;

#nullable disable

namespace SwiftHR.Models
{
    public partial class UserDetail
    {
        public int UserId { get; set; }
        public int? RoleId { get; set; }
        public int? EmployeeId { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public string ProfilePicturePath { get; set; }
        public bool? IsPwdChangeFt { get; set; }
        public string CreatedDate { get; set; }
    }
}
