using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Bcvp.Blog.Core.BlogCore.Posts
{
    public class GetPostInput
    {
        [Required]
        public string Url { get; set; }

        public Guid BlogId { get; set; }
    }
}
