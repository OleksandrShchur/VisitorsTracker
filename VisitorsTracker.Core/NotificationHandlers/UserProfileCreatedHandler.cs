using MediatR;
using System.Threading;
using System.Threading.Tasks;
using VisitorsTracker.Core.DTOs;
using VisitorsTracker.Core.Extensions;
using VisitorsTracker.Core.IServices;
using VisitorsTracker.Core.Notifications;

namespace VisitorsTracker.Core.NotificationHandlers
{
    public class UserProfileCreatedHandler : INotificationHandler<UserProfileCreatedMessage>
    {
        private readonly IEmailService _sender;
        private readonly IUserService _userService;

        public UserProfileCreatedHandler(
            IEmailService sender,
            IUserService userService)
        {
            _sender = sender;
            _userService = userService;
        }

        public async Task Handle(UserProfileCreatedMessage notification, CancellationToken cancellationToken)
        {
            var users = _userService.GetById(notification.User.Id);

            string link = $"{AppHttpContext.AppBaseUrl}";
            await _sender.SendEmailAsync(new EmailDTO
            {
                Subject = "Перший вхід у Visitors Tracker",
                RecepientEmail = notification.User.Email,
                MessageText = $"Visitors Tracker - система автоматичного ведення обліку відвідуваності" +
                $"студентів факультету. Щоб відвідати сайт, перейдіть <a href='{link}'>за посиланням</a>" +
                $". З повагою, Visitors Tracker",
            });
        }
    }
}
