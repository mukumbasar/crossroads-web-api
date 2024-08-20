using Crossroads.Application.Dtos.Configurations;
using Crossroads.Application.Interfaces.Services;
using Crossroads.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossroads.Application.Extensions
{
    public static class EmailExtensions
    {
        public static IServiceCollection AddEmailExtensions(this IServiceCollection services, IConfiguration configuration)
        {
            // Bind EmailOptions from the configuration
            var emailOptions = configuration.GetSection("EmailOptions").Get<EmailOptions>();

            // Register EmailQueueService with the retrieved configuration values
            services.AddSingleton<IEmailQueueService>(provider => new EmailQueueService(emailOptions.Hostname, emailOptions.QueueName));

            // Register EmailConsumerService with the retrieved configuration values
            services.AddSingleton<IEmailConsumerService>(provider =>
                new EmailConsumerService(
                    emailOptions.Hostname,
                    emailOptions.QueueName,
                    emailOptions.SmtpServer,
                    emailOptions.SmtpPort,
                    emailOptions.SmtpUser,
                    emailOptions.SmtpPass));

            // Trigger RabbitMQ consumer
            var serviceProvider = services.BuildServiceProvider();
            var emailConsumerService = serviceProvider.GetRequiredService<IEmailConsumerService>();
            var consumingTask = emailConsumerService.StartConsumingAsync();

            return services;
        }

        public static async Task StartEmailConsumerAsync(IServiceProvider serviceProvider)
        {
            var emailConsumerService = serviceProvider.GetRequiredService<IEmailConsumerService>();
            await emailConsumerService.StartConsumingAsync();
        }
    }
}
