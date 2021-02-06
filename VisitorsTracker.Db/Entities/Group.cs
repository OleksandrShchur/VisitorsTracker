using System;
using System.Collections.Generic;

namespace VisitorsTracker.Db.Entities
{
    public class Group : BaseEntity
    {
        public Guid UserId { get; set; }

        public IEnumerable<User> User { get; set; }

        public string Number { get; set; }
    }
}
