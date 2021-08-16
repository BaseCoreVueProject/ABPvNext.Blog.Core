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

namespace Bcvp.Blog.Core.BlogCore.Blogs
{
    public class EfCoreBlogRepository : EfCoreRepository<CoreDbContext, Blog, Guid>, IBlogRepository
    {
        public EfCoreBlogRepository(IDbContextProvider<CoreDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<Blog> FindByShortNameAsync(string shortName, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync()).FirstOrDefaultAsync(p => p.ShortName == shortName, GetCancellationToken(cancellationToken));
        }
    }
}
