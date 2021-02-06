using System;

namespace VisitorsTracker.Core.Exceptions
{
    [Serializable]
    public class VisitorsTrackerException : Exception
    {
        public VisitorsTrackerException()
        {
        }

        public VisitorsTrackerException(string message)
            : base(message)
        {
        }

        public VisitorsTrackerException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
