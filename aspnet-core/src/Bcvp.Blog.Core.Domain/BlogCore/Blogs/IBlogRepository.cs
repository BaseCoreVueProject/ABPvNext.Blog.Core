using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Bcvp.Blog.Core.BlogCore.Blogs
{
    public interface IBlogRepository : IBasicRepository<Blog, Guid>
    {
        Task<Blog> FindByShortNameAsync(string shortName, CancellationToken cancellationToken = default);
    }
}
