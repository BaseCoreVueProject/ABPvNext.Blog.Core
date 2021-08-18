using System;
using System.Collections.Generic;
using System.Text;

namespace Bcvp.Blog.Core.BlogCore.Files
{
    public class BlogWebConsts
    {
        public class FileUploading
        {
            /// <summary>
            /// Default value: 5242880
            /// </summary>
            public static int MaxFileSize { get; set; } = 5242880; //5MB

            public static int MaxFileSizeAsMegabytes => Convert.ToInt32((MaxFileSize / 1024f) / 1024f);
        }
    }
}
