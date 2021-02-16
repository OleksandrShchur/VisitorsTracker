using System;
using System.Collections.Generic;
using VisitorsTracker.Db.Entities;
using VisitorsTracker.Db.Enums;

namespace VisitorsTracker.Core.DTOs
{
    public class UserDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string PasswordHash { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public DateTime Birthday { get; set; }

        public Gender Gender { get; set; }

        public virtual Guid RoleId { get; set; }

        public string PhotoUrl { get; set; }

        public IEnumerable<RefreshToken> RefreshTokens { get; set; }
    }
}
