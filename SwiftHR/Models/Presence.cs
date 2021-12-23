using System;
using System.Collections.Generic;

#nullable disable

namespace SwiftHR.Models
{
    public partial class Presence
    {
        public char PresenceId { get; set; }
        public string PresenceName { get; set; }        
        public bool IsActive { get; set; }
    }
}
