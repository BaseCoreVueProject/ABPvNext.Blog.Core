using System.ComponentModel.DataAnnotations;

namespace Bcvp.Blog.Core.BlogCore.Comments
{
    public class UpdateCommentDto
    {
        [Required]
        public string Text { get; set; }
    }
}
