using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bcvp.Blog.Core.BlogCore.Blogs;
using Shouldly;
using Volo.Abp.Identity;
using Xunit;

namespace Bcvp.Blog.Core.BlogCore
{
    public class BlogAppService_Tests : CoreApplicationTestBase
    {
        private readonly IBlogAppService _blogAppService;
        public BlogAppService_Tests()
        {
            _blogAppService = GetRequiredService<IBlogAppService>();
        }

        [Fact]
        public async Task Should_Get_List_Of_Blogs()
        {
            var blogs = await _blogAppService.GetListAsync();

            blogs.Items.Count.ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task Should_Get_Blog_By_Shortname()
        {
            var targetBlog = (await _blogAppService.GetListAsync()).Items.First();

            var blog = await _blogAppService.GetByShortNameAsync(targetBlog.ShortName);

            blog.ShouldNotBeNull();
            blog.Name.ShouldBe(targetBlog.Name);
        }

    }
}
