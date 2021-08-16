using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bcvp.Blog.Core.BlogCore.Posts
{
    public class PostChangedEvent
    {
        public Guid BlogId { get; set; }
    }
}
