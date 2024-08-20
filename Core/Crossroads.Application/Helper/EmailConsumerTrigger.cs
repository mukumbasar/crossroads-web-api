using Crossroads.Application.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossroads.Application.Helper
{
    public static class EmailConsumerTrigger
    {
        public static async Task StartEmailConsumerAsync(IServiceProvider serviceProvider)
        {
            var emailConsumerService = serviceProvider.GetRequiredService<IEmailConsumerService>();
            await emailConsumerService.StartConsumingAsync();
        }
    }
}
