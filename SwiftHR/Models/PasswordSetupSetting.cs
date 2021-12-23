using System;
using System.Collections.Generic;

#nullable disable

namespace SwiftHR.Models
{
    public partial class PasswordSetupSetting
    {
        public int PasswordSettingId { get; set; }
        public int? MinimumPasswordLength { get; set; }
        public int? PasswordExpiryLimitInDays { get; set; }
        public int? ExpiryReminderInDays { get; set; }
        public int? PreviousPasswordAllowCount { get; set; }
        public int? AllowedInvalidLoginAttemptsCount { get; set; }
        public int? WelcomeEmailPasswordLinkExpiryInDays { get; set; }
        public int? AllowUserToChangePasswordOnExpiry { get; set; }
    }
}
