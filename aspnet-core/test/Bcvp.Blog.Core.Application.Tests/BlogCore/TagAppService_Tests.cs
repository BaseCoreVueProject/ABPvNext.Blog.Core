using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bcvp.Blog.Core.BlogCore.Tagging;
using Shouldly;
using Xunit;

namespace Bcvp.Blog.Core.BlogCore
{
    public class TagAppService_Tests : CoreApplicationTestBase
    {
        private readonly ITagAppService _tagAppService;
        private readonly ITagRepository _tagRepository;
        private readonly CoreTestData _bloggingTestData;

        public TagAppService_Tests()
        {
            _tagAppService = GetRequiredService<ITagAppService>();
            _tagRepository = GetRequiredService<ITagRepository>();
            _bloggingTestData = GetRequiredService<CoreTestData>();
        }

        [Fact]
        public async Task Should_Get_Popular_Tags()
        {
            var tags = await _tagAppService.GetPopularTagsAsync(_bloggingTestData.Blog1Id,
                new GetPopularTagsInput() { ResultCount = 5, MinimumPostCount = 0 });

            tags.Count.ShouldBeGreaterThan(0);
        }
    }
}
