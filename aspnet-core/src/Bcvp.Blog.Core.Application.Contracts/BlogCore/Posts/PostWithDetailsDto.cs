using System;
using System.Collections.Generic;
using System.Text;
using Bcvp.Blog.Core.BlogCore.Tagging;
using JetBrains.Annotations;
using Volo.Abp.Application.Dtos;

namespace Bcvp.Blog.Core.BlogCore.Posts
{
    public class PostWithDetailsDto : FullAuditedEntityDto<Guid>
    {
        public Guid BlogId { get; set; }

        public string Title { get; set; }

        public string CoverImage { get; set; }

        public string Url { get; set; }

        public string Content { get; set; }

        public string Description { get; set; }

        public int ReadCount { get; set; }

        public int CommentCount { get; set; }

        [CanBeNull]
        public BlogUserDto Writer { get; set; }

        public List<TagDto> Tags { get; set; }
    }
}
