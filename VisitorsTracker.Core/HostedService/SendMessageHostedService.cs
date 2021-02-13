﻿using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using VisitorsTracker.Core.IServices;

namespace VisitorsTracker.Core.HostedService
{
    public class SendMessageHostedService : BackgroundService
    {
        private readonly ILogger<SendMessageHostedService> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public SendMessageHostedService(
            IServiceProvider services,
            ILogger<SendMessageHostedService> logger,
            IMediator mediator,
            IMapper mapper)
        {
            Services = services;
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
        }

        public IServiceProvider Services { get; }

        private async Task DoWork(CancellationToken stoppingToken)
        {
            using var scope = Services.CreateScope();
            var scopedProcessingService =
                scope.ServiceProvider
                    .GetRequiredService<IUserService>();

            /*while (!stoppingToken.IsCancellationRequested)
            {
                var events = scopedProcessingService.GetUrgentEventSchedules();
                try
                {
                    foreach (var ev in events)
                    {
                        //await _mediator.Publish(new CreateEventVerificationMessage(_mapper.Map<EventScheduleDTO>(ev)));
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }

                await Task.Delay(1000 * 60 * 60 * 24, stoppingToken);

                _logger.LogInformation("Message Hosted Service is working.");
            }*/
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
            "Message Service Hosted Service is stopping.");

            await base.StopAsync(stoppingToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
            "Message Service Hosted Service running.");

            await DoWork(stoppingToken);
        }
    }
}
