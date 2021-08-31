using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bcvp.Blog.Core.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Bcvp.Blog.Core.BlogCore.Comments
{
    public class EfCoreCommentRepository:EfCoreRepository<CoreDbContext, Comment, Guid>,ICommentRepository
    {
        public EfCoreCommentRepository(IDbContextProvider<CoreDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<List<Comment>> GetListOfPostAsync(Guid postId, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .Where(a => a.PostId == postId)
                .OrderBy(a => a.CreationTime)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<int> GetCommentCountOfPostAsync(Guid postId, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .CountAsync(a => a.PostId == postId, GetCancellationToken(cancellationToken));
        }

        public async Task<List<Comment>> GetRepliesOfComment(Guid id, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .Where(a => a.RepliedCommentId == id).ToListAsync(GetCancellationToken(cancellationToken));
        }

        public async Task DeleteOfPost(Guid id, CancellationToken cancellationToken = default)
        {
            var recordsToDelete = await (await GetDbSetAsync()).Where(pt => pt.PostId == id).ToListAsync(GetCancellationToken(cancellationToken));
            (await GetDbSetAsync()).RemoveRange(recordsToDelete);
        }
    }
}
