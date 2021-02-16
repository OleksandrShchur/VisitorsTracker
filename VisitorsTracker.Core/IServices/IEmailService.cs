﻿using System.Threading.Tasks;
using VisitorsTracker.Core.DTOs;

namespace VisitorsTracker.Core.IServices
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailDTO emailDTO);
    }
}
