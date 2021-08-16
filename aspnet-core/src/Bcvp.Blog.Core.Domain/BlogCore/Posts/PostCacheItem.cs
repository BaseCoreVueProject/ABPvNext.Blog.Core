using Bcvp.Blog.Core.BlogCore.Tagging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Auditing;

namespace Bcvp.Blog.Core.BlogCore.Posts
{
    [Serializable]
    public class PostCacheItem : ICreationAuditedObject
    {
        public Guid Id { get; set; }

        public Guid BlogId { get; set; }

        public string Title { get; set; }

        public string CoverImage { get; set; }

        public string Url { get; set; }

        public string Content { get; set; }

        public string Description { get; set; }

        public int ReadCount { get; set; }

        public int CommentCount { get; set; }

        public List<Tag> Tags { get; set; }

        public Guid? CreatorId { get; set; }

        public DateTime CreationTime { get; set; }
    }
}
