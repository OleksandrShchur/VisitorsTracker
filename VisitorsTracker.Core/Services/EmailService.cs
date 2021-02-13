using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using VisitorsTracker.Core.DTOs;
using VisitorsTracker.Core.Infrastructure;
using VisitorsTracker.Core.IServices;

namespace VisitorsTracker.Core.Services
{
    public class EmailService : IEmailService
    {
        private readonly IOptions<EmailOptionsModel> _senderOptions;

        public EmailService(IOptions<EmailOptionsModel> opt)
        {
            _senderOptions = opt;
        }

        public Task SendEmailAsync(EmailDTO emailDto)
        {
            var from = _senderOptions.Value.Account;
            var pass = _senderOptions.Value.Password;

            var client = new SmtpClient
            {
                Host = _senderOptions.Value.Host,
                Port = _senderOptions.Value.Port,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = _senderOptions.Value.UseDefaultCredentials,
                Credentials = (!_senderOptions.Value.UseDefaultCredentials) ? new NetworkCredential(@from, pass) : null,
                EnableSsl = _senderOptions.Value.EnableSsl,
            };

            var mail = new MailMessage(@from, emailDto.RecepientEmail)
            {
                Subject = emailDto.Subject,
                Body = emailDto.MessageText,
                IsBodyHtml = true,
            };
            return client.SendMailAsync(mail);
        }
    }
}
