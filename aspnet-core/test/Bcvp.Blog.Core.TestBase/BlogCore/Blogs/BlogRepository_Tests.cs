using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shouldly;
using Volo.Abp.Modularity;
using Xunit;

namespace Bcvp.Blog.Core.BlogCore.Blogs
{
    public abstract class BlogRepository_Tests<TStartupModule> : CoreTestBase<TStartupModule>
        where TStartupModule : IAbpModule
    {
        protected IBlogRepository BlogRepository { get; }

        protected BlogRepository_Tests()
        {
            BlogRepository = GetRequiredService<IBlogRepository>();
        }

        [Fact]
        public async Task Should_Find_By_ShortName()
        {
            var blogFromRepository = (await BlogRepository.GetListAsync()).First();
            var blog = await BlogRepository.FindByShortNameAsync(blogFromRepository.ShortName);
            blog.ShouldNotBeNull();
        }
    }
}
