using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Bcvp.Blog.Core.BlogCore.Comments
{
    public class Comment_Tests
    {
        [Theory]
        [InlineData("aaa")]
        [InlineData("bbb")]
        public void SetText(string text)
        {
            var comment = new Comment(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), "good");
            comment.SetText(text);
            comment.Text.ShouldBe(text);
        }
    }
}
