using MediatR;
using VisitorsTracker.Core.DTOs;

namespace VisitorsTracker.Core.Notifications
{
    public class UserProfileCreatedMessage : INotification
    {
        public UserProfileCreatedMessage(UserDTO user)
        {
            User = user;
        }

        public UserDTO User { get; set; }
    }
}
