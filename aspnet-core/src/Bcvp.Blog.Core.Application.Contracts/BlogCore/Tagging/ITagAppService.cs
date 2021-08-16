using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Bcvp.Blog.Core.BlogCore.Tagging
{
    public interface ITagAppService : IApplicationService
    {
        Task<List<TagDto>> GetPopularTags(Guid blogId, GetPopularTagsInput input);

    }
}
