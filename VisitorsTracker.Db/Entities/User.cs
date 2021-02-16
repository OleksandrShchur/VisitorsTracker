using System;
using System.Collections.Generic;
using VisitorsTracker.Db.Enums;

namespace VisitorsTracker.Db.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }

        public string PasswordHash { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public DateTime Birthday { get; set; }

        public Gender Gender { get; set; }

        public Guid RoleId { get; set; }

        public virtual Role Role { get; set; }

        public string Photo { get; set; }

        public Guid GroupId { get; set; }

        public virtual Group Group { get; set; }

        public IEnumerable<RefreshToken> RefreshTokens { get; set; }
    }
}
