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
using Volo.Abp.Users.EntityFrameworkCore;

namespace Bcvp.Blog.Core.BlogCore.Users
{
    public class EfCoreBlogUserRepository : EfCoreUserRepositoryBase<CoreDbContext, BlogUser>, IBlogUserRepository
    {
        public EfCoreBlogUserRepository(IDbContextProvider<CoreDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<BlogUser>> GetUsersAsync(int maxCount, string filter, CancellationToken cancellationToken = default)
        {
            return await DbSet
                .WhereIf(!string.IsNullOrWhiteSpace(filter), x => x.UserName.Contains(filter))
                .Take(maxCount).ToListAsync(cancellationToken);
        }
    }
}
