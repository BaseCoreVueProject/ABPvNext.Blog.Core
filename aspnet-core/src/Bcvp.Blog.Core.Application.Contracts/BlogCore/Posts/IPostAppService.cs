using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Bcvp.Blog.Core.BlogCore.Posts
{
    public interface IPostAppService : IApplicationService
    {
        Task<ListResultDto<PostWithDetailsDto>> GetListByBlogIdAndTagName(Guid blogId, string tagName);

        Task<ListResultDto<PostWithDetailsDto>> GetTimeOrderedListAsync(Guid blogId);

        Task<PostWithDetailsDto> GetForReadingAsync(GetPostInput input);

        Task<PostWithDetailsDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<PostWithDetailsDto> CreateAsync(CreatePostDto input);

        Task<PostWithDetailsDto> UpdateAsync(Guid id, UpdatePostDto input);
    }
}
