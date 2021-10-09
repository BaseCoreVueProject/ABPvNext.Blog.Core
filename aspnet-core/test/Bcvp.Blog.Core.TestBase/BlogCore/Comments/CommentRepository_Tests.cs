using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shouldly;
using Volo.Abp.Modularity;
using Xunit;

namespace Bcvp.Blog.Core.BlogCore.Comments
{
    public abstract class CommentRepository_Tests<TStartupModule> : CoreTestBase<TStartupModule>
        where TStartupModule : IAbpModule
    {
        protected ICommentRepository CommentRepository { get; }
        protected CoreTestData BloggingTestData { get; }

        protected CommentRepository_Tests()
        {
            CommentRepository = GetRequiredService<ICommentRepository>();
            BloggingTestData = GetRequiredService<CoreTestData>();
        }

        [Fact]
        public async Task GetListOfPostAsync()
        {
            var comments = await CommentRepository.GetListOfPostAsync(BloggingTestData.Blog1Post1Id);
            comments.ShouldNotBeNull();
            comments.Count.ShouldBe(2);
            comments.ShouldAllBe(x => x.PostId == BloggingTestData.Blog1Post1Id);
        }

        [Fact]
        public async Task GetCommentCountOfPostAsync()
        {
            var count = await CommentRepository.GetCommentCountOfPostAsync(BloggingTestData.Blog1Post1Id);
            count.ShouldBe(2);
        }

        [Fact]
        public async Task GetRepliesOfComment()
        {
            var comment = await CommentRepository.GetRepliesOfComment(BloggingTestData.Blog1Post1Comment1Id);
            comment.ShouldNotBeNull();
            comment.ShouldContain(x => x.Id == BloggingTestData.Blog1Post1Comment2Id);
        }

        [Fact]
        public async Task DeleteOfPost()
        {
            await CommentRepository.DeleteOfPost(BloggingTestData.Blog1Post1Id);
            (await CommentRepository.GetListAsync()).ShouldBeEmpty();
        }
    }
}
