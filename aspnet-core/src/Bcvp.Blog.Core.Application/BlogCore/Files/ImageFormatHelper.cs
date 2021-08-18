using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bcvp.Blog.Core.BlogCore.Files
{
    public class ImageFormatHelper
    {
        public static ImageFormat GetImageRawFormat(byte[] fileBytes)
        {
            using (var memoryStream = new MemoryStream(fileBytes))
            {
                return System.Drawing.Image.FromStream(memoryStream).RawFormat;
            }
        }

        public static bool IsValidImage(byte[] fileBytes, ICollection<ImageFormat> validFormats)
        {
            var imageFormat = GetImageRawFormat(fileBytes);
            return validFormats.Contains(imageFormat);
        }
    }
}
