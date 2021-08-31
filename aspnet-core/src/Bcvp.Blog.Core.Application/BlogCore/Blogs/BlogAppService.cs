using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace Bcvp.Blog.Core.BlogCore.Blogs
{
    public class BlogAppService : CoreAppService, IBlogAppService
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BlogAppService(IBlogRepository blogRepository, IHttpContextAccessor httpContextAccessor)
        {
            _blogRepository = blogRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ListResultDto<BlogDto>> GetListAsync()
        {
            var blogs = await _blogRepository.GetListAsync();

            return new ListResultDto<BlogDto>(
                ObjectMapper.Map<List<Blog>, List<BlogDto>>(blogs)
            );
        }


        public async Task<BlogDto> GetByShortNameAsync(string shortName)
        {
            Check.NotNullOrWhiteSpace(shortName, nameof(shortName));

           
            var blog = await _blogRepository.FindByShortNameAsync(shortName);

            if (blog == null)
            {
                throw new EntityNotFoundException(typeof(Blog), shortName);
            }

            return ObjectMapper.Map<Blog, BlogDto>(blog);
        }

        public async Task<BlogDto> GetAsync(Guid id)
        {
            var blog = await _blogRepository.GetAsync(id);

            return ObjectMapper.Map<Blog, BlogDto>(blog);
        }
    }
}
