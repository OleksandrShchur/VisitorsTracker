using MediatR;
using System.Threading;
using System.Threading.Tasks;
using VisitorsTracker.Core.DTOs;
using VisitorsTracker.Core.Exceptions;
using VisitorsTracker.Core.Extensions;
using VisitorsTracker.Core.IServices;
using VisitorsTracker.Core.Notifications;

namespace VisitorsTracker.Core.NotificationHandlers
{
    public class UserProfileCreatedHandler : INotificationHandler<UserProfileCreatedMessage>
    {
        private readonly IEmailService _sender;

        public UserProfileCreatedHandler(IEmailService sender)
        {
            _sender = sender;
        }

        public async Task Handle(UserProfileCreatedMessage notification, CancellationToken cancellationToken)
        {
            string link = $"{AppHttpContext.AppBaseUrl}";
            bool mailSent = await _sender.SendEmailAsync(new EmailDTO
            {
                Subject = "Перший вхід у Visitors Tracker",
                RecepientEmail = notification.User.Email,
                MessageText = $"Visitors Tracker - система автоматичного ведення обліку відвідуваності" +
                $"студентів факультету. Щоб відвідати сайт, перейдіть <a href='{link}'>за посиланням</a>" +
                $". З повагою, Visitors Tracker",
            });

            if (!mailSent)
            {
                throw new VisitorsTrackerException("Email not sent.");
            }
        }
    }
}
