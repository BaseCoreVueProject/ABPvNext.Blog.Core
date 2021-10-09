using System;
using Bcvp.Blog.Core.EntityFrameworkCore;

namespace Bcvp.Blog.Core
{
    public abstract class CoreApplicationTestBase : CoreTestBase<CoreApplicationTestModule> 
    {
        protected virtual void UsingDbContext(Action<CoreDbContext> action)
        {
            using (var dbContext = GetRequiredService<CoreDbContext>())
            {
                action.Invoke(dbContext);
            }
        }

        protected virtual T UsingDbContext<T>(Func<CoreDbContext, T> action)
        {
            using (var dbContext = GetRequiredService<CoreDbContext>())
            {
                return action.Invoke(dbContext);
            }
        }
    }
}
