using System;
using System.Collections.Generic;
using System.Text;

namespace Bcvp.Blog.Core.BlogCore.Tagging
{
    public class GetPopularTagsInput
    {
        public int ResultCount { get; set; } = 10;

        public int? MinimumPostCount { get; set; }
    }
}
