using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shouldly;
using Volo.Abp.Modularity;
using Xunit;

namespace Bcvp.Blog.Core.BlogCore.Posts
{
    public abstract class PostRepository_Tests<TStartupModule> : CoreTestBase<TStartupModule>
        where TStartupModule : IAbpModule
    {
        protected IPostRepository PostRepository { get; }
        protected CoreTestData BloggingTestData { get; }

        protected PostRepository_Tests()
        {
            PostRepository = GetRequiredService<IPostRepository>();
            BloggingTestData = GetRequiredService<CoreTestData>();
        }

        [Fact]
        public async Task GetListOfPostAsync()
        {
            var posts = await PostRepository.GetPostsByBlogId(BloggingTestData.Blog1Id);
            posts.ShouldNotBeNull();
            posts.Count.ShouldBe(2);
            posts.ShouldContain(x => x.Id == BloggingTestData.Blog1Post1Id);
            posts.ShouldContain(x => x.Id == BloggingTestData.Blog1Post2Id);
        }

        [Fact]
        public async Task GetPostByUrl()
        {
            var post = await PostRepository.GetPostByUrl(BloggingTestData.Blog1Id, "url");
            post.ShouldNotBeNull();
            post.Url.ShouldBe("url");
        }
    }
}
