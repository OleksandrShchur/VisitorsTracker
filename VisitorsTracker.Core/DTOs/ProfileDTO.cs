using System;
using VisitorsTracker.Db.Enums;

namespace VisitorsTracker.Core.DTOs
{
    public class ProfileDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public DateTime Birthday { get; set; }

        public Gender Gender { get; set; }

        public string UserPhoto { get; set; }
    }
}
