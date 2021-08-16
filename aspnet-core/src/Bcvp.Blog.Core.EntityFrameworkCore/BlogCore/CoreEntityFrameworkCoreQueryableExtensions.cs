using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bcvp.Blog.Core.BlogCore.Posts;
using Microsoft.EntityFrameworkCore;

namespace Bcvp.Blog.Core.BlogCore
{
    public static class CoreEntityFrameworkCoreQueryableExtensions
    {
        public static IQueryable<Post> IncludeDetails(this IQueryable<Post> queryable, bool include = true)
        {
            if (!include)
            {
                return queryable;
            }

            return queryable
                .Include(x => x.Tags);
        }
    }
}
