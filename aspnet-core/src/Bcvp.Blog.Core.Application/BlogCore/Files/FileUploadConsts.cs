using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bcvp.Blog.Core.BlogCore.Files
{
    public class FileUploadConsts
    {
        public static readonly ICollection<ImageFormat> AllowedImageUploadFormats = new Collection<ImageFormat>
        {
            ImageFormat.Jpeg,
            ImageFormat.Png,
            ImageFormat.Gif,
            ImageFormat.Bmp
        };

        public static string AllowedImageFormatsJoint => string.Join(",", AllowedImageUploadFormats.Select(x => x.ToString()));
    }
}
