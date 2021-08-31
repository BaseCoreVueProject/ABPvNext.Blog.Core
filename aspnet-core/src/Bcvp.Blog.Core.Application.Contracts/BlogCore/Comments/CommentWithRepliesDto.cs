using System.Collections.Generic;

namespace Bcvp.Blog.Core.BlogCore.Comments
{
    public class CommentWithRepliesDto
    {
        public CommentWithDetailsDto Comment { get; set; }

        public List<CommentWithDetailsDto> Replies { get; set; } = new List<CommentWithDetailsDto>();
    }
}
