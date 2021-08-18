﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bcvp.Blog.Core.BlogCore.Files
{
    public class FileUploadInputDto
    {
        [Required]
        public byte[] Bytes { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
