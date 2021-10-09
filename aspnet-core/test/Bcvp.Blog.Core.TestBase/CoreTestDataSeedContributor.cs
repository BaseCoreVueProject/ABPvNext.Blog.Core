using System;
using System.Threading.Tasks;
using Bcvp.Blog.Core.BlogCore.Blogs;
using Bcvp.Blog.Core.BlogCore.Comments;
using Bcvp.Blog.Core.BlogCore.Posts;
using Bcvp.Blog.Core.BlogCore.Tagging;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;

namespace Bcvp.Blog.Core
{
    public class CoreTestDataSeedContributor : IDataSeedContributor, ITransientDependency
    {

        private readonly CoreTestData _testData;
        private readonly IBlogRepository _blogRepository;
        private readonly IPostRepository _postRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly ITagRepository _tagRepository;
        private readonly ICurrentTenant _currentTenant;

        public CoreTestDataSeedContributor(
            CoreTestData testData,
            IBlogRepository blogRepository,
            IPostRepository postRepository,
            ICommentRepository commentRepository,
            ITagRepository tagRepository,
            ICurrentTenant currentTenant)
        {
            _testData = testData;
            _blogRepository = blogRepository;
            _postRepository = postRepository;
            _commentRepository = commentRepository;
            _tagRepository = tagRepository;
            _currentTenant = currentTenant;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            /* Seed additional test data... */
            using (_currentTenant.Change(context?.TenantId))
            {
                await SeedBlogsAsync();
                await SeedPostsAsync();
                await SeedCommentsAsync();
                await SeedTagsAsync();

            }
        }


        private async Task SeedBlogsAsync()
        {
            await _blogRepository.InsertAsync(new BlogCore.Blogs.Blog(_testData.Blog1Id, "The First Blog", "blog-1"));
        }

        private async Task SeedPostsAsync()
        {
            await _postRepository.InsertAsync(new Post(_testData.Blog1Post1Id, _testData.Blog1Id, "title", "coverImage", "url"));
            await _postRepository.InsertAsync(new Post(_testData.Blog1Post2Id, _testData.Blog1Id, "title2", "coverImage2", "url2"));
        }

        public async Task SeedCommentsAsync()
        {
            await _commentRepository.InsertAsync(new Comment(_testData.Blog1Post1Comment1Id, _testData.Blog1Post1Id, null, "text"));
            await _commentRepository.InsertAsync(new Comment(_testData.Blog1Post1Comment2Id, _testData.Blog1Post1Id, _testData.Blog1Post1Comment1Id, "text"));
        }

        public async Task SeedTagsAsync()
        {
            await _tagRepository.InsertAsync(new Tag(Guid.NewGuid(), _testData.Blog1Id, _testData.Tag1Name, 10));
            await _tagRepository.InsertAsync(new Tag(Guid.NewGuid(), _testData.Blog1Id, _testData.Tag2Name));
        }
    }
}