using System;
using System.Collections.Generic;
using VisitorsTracker.Db.Enums;

namespace VisitorsTracker.Db.Entities
{
    public class Visiting : BaseEntity
    {
        public Guid ClassesId { get; set; }

        public virtual Classes Classes { get; set; }

        public Attendance Attendance { get; set; }

        public Guid UserId { get; set; }

        public virtual User Users { get; set; }

        public DateTime Date { get; set; }
    }
}
