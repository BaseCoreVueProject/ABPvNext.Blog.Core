using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Bcvp.Blog.Core.BlogCore.Comments
{
    public interface ICommentAppService : IApplicationService
    {
        Task<List<CommentWithRepliesDto>> GetHierarchicalListOfPostAsync(Guid postId);

        Task<CommentWithDetailsDto> CreateAsync(CreateCommentDto input);

        Task<CommentWithDetailsDto> UpdateAsync(Guid id, UpdateCommentDto input);

        Task DeleteAsync(Guid id);
    }
}
