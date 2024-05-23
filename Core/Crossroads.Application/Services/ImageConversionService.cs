using Crossroads.Application.Helper;
using Crossroads.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossroads.Application.Services
{
    public class ImageConversionService : IImageConversionService
    {
        static readonly List<byte> jpg = new List<byte> { 0xFF, 0xD8 };
        static readonly List<byte> bmp = new List<byte> { 0x42, 0x4D };
        static readonly List<byte> gif = new List<byte> { 0x47, 0x49, 0x46 };
        static readonly List<byte> png = new List<byte> { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A };
        static readonly List<byte> svg_xml_small = new List<byte> { 0x3C, 0x3F, 0x78, 0x6D, 0x6C }; // "<?xml"
        static readonly List<byte> svg_xml_capital = new List<byte> { 0x3C, 0x3F, 0x58, 0x4D, 0x4C }; // "<?XML"
        static readonly List<byte> svg_small = new List<byte> { 0x3C, 0x73, 0x76, 0x67 }; // "<svg"
        static readonly List<byte> svg_capital = new List<byte> { 0x3C, 0x53, 0x56, 0x47 }; // "<SVG"
        static readonly List<byte> intel_tiff = new List<byte> { 0x49, 0x49, 0x2A, 0x00 };
        static readonly List<byte> motorola_tiff = new List<byte> { 0x4D, 0x4D, 0x00, 0x2A };

        static readonly List<(List<byte> magic, string extension)> imageFormats = new List<(List<byte> magic, string extension)>()
    {
        (jpg, "jpg"),
        (bmp, "bmp"),
        (gif, "gif"),
        (png, "png"),
        (svg_small, "svg"),
        (svg_capital, "svg"),
        (intel_tiff,"tif"),
        (motorola_tiff, "tif"),
        (svg_xml_small, "svg"),
        (svg_xml_capital, "svg")
    };

        public async Task<byte[]> ConvertToByteArrayAsync(IFormFile file)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);

                return memoryStream.ToArray();
            }
        }

        public async Task<string> ConvertToIFormFile(byte[] fileBytes)
        {
            using (MemoryStream memoryStream = new MemoryStream(fileBytes))
            {
                var extension = ImageConversionHelper.GetExtension(fileBytes);

                var base64 = Convert.ToBase64String(fileBytes);

                if (extension != null)
                {
                    return string.Format("data:image/jpg;base64,{0}", base64);
                }

                return string.Format("data:application/pdf;base64,{0}", base64);
            }
        }
    }
}
