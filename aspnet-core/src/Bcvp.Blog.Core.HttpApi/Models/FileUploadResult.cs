using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bcvp.Blog.Core.Models
{
    public class FileUploadResult
    {
        public string FileUrl { get; set; }

        public FileUploadResult(string fileUrl)
        {
            FileUrl = fileUrl;
        }
    }
}
