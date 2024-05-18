using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossroads.Application.Interfaces.Services
{
    public interface IEmailService
    {
        void SendActivationEmailAsync(string email, string token);
    }
}
