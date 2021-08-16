using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Bcvp.Blog.Core.BlogCore.Comments
{
    public interface ICommentRepository : IBasicRepository<Comment, Guid>
    {
        Task DeleteOfPost(Guid id, CancellationToken cancellationToken = default);
    }
}
