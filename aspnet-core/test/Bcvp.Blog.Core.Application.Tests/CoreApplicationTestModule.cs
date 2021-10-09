using Bcvp.Blog.Core.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Bcvp.Blog.Core
{
    [DependsOn(
        typeof(CoreApplicationModule),
        typeof(CoreEntityFrameworkCoreTestModule),
        typeof(CoreTestBaseModule)
        )]
    public class CoreApplicationTestModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAlwaysAllowAuthorization();
        }
    }
}