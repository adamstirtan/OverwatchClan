using System;
using System.Collections.Generic;

namespace Clan.ObjectModel
{
    public class User : BaseEntity
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime? LastLogin { get; set; }
        public bool LockedOut { get; set; }
        public int FailedLoginAttempts { get; set; }

        public ICollection<Role> Roles { get; set; }
    }
}