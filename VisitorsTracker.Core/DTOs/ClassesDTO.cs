using System;
using System.Collections.Generic;
using System.Text;
using VisitorsTracker.Db.Entities;
using VisitorsTracker.Db.Enums;

namespace VisitorsTracker.Core.DTOs
{
    public class ClassesDTO
    {
        public Guid Id { get; set; }

        public Guid ProfessorId { get; set; }

        public Guid GroupId { get; set; }

        public virtual Group Group { get; set; }

        public virtual User Professor { get; set; }

        public Guid SubjectId { get; set; }

        public virtual Subjects Subjects { get; set; }

        public TypeOfClasses Type { get; set; }

        public Frequency Frequency { get; set; }

        public NumberOfClasses Number { get; set; }

        public DayWeek DayWeek { get; set; }
    }
}
