using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossroads.Application.Interfaces.Services
{
    public interface IEmailQueueService
    {
        Task QueueMessageAsync(string email, string subject, string body);
    }
}
