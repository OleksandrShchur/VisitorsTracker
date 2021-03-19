using System;
using System.Collections.Generic;
using VisitorsTracker.Db.Enums;

namespace VisitorsTracker.Db.Entities
{
    public class Classes : BaseEntity
    {
        public Guid GroupId { get; set; }

        public virtual IEnumerable<Group> Groups { get; set; }

        public Guid UserId { get; set; }

        public Guid SubjectId { get; set; }

        public virtual ICollection<Subjects> Subjects { get; set; }

        public Frequency Frequency { get; set; }

        public NumberOfClasses Number { get; set; }

        public DayWeek DayWeek { get; set; }

        public TypeOfClasses Type { get; set; }

    }
}
