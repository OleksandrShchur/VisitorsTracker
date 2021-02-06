using System;

namespace VisitorsTracker.ViewModels
{
    public class UserPreviewViewModel
    {
        public Guid Id { get; set; }

        public string PhotoUrl { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public DateTime Birthday { get; set; }
    }
}
