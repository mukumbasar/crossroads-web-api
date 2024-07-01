using Crossroads.Application.Interfaces.Services;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossroads.Application.Services
{
    public class EmailConsumerBackgroundService : BackgroundService
    {
        private readonly IEmailConsumerService _emailConsumerService;

        public EmailConsumerBackgroundService(IEmailConsumerService emailConsumerService)
        {
            _emailConsumerService = emailConsumerService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Consume emails from the queue
            await _emailConsumerService.StartConsumingAsync();

            // Keep it running if it is not canceleld
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken); 
            }
        }
    }
}
