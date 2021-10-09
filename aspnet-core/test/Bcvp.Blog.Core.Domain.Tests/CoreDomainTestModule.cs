using Bcvp.Blog.Core.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Bcvp.Blog.Core
{
    [DependsOn(
        typeof(CoreEntityFrameworkCoreTestModule),
        typeof(CoreTestBaseModule)
        )]
    public class CoreDomainTestModule : AbpModule
    {

    }
}