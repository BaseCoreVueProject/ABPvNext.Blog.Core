using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Bcvp.Blog.Core.BlogCore.Tagging
{
    public interface ITagRepository : IBasicRepository<Tag, Guid>
    {
        Task<List<Tag>> GetListAsync(Guid blogId, CancellationToken cancellationToken = default);

        Task<Tag> GetByNameAsync(Guid blogId, string name, CancellationToken cancellationToken = default);

        Task<Tag> FindByNameAsync(Guid blogId, string name, CancellationToken cancellationToken = default);

        Task<List<Tag>> GetListAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);

        Task DecreaseUsageCountOfTagsAsync(List<Guid> id, CancellationToken cancellationToken = default);
    }
}
