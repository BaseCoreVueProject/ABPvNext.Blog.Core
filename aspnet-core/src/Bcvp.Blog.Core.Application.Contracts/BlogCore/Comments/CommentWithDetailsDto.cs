using System;
using Bcvp.Blog.Core.BlogCore.Posts;
using Volo.Abp.Application.Dtos;

namespace Bcvp.Blog.Core.BlogCore.Comments
{
    public class CommentWithDetailsDto : FullAuditedEntityDto<Guid>
    {
        public Guid? RepliedCommentId { get; set; }

        public string Text { get; set; }

        public BlogUserDto Writer { get; set; }
    }
}
