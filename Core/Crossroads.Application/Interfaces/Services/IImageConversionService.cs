using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossroads.Application.Interfaces.Services
{
    public interface IImageConversionService
    {
        Task<byte[]> ConvertToByteArrayAsync(IFormFile file);
        Task<string> ConvertToIFormFile(byte[] fileBytes);
    }
}
