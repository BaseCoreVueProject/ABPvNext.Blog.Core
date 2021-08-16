using Volo.Abp.Modularity;

namespace Bcvp.Blog.Core
{
    [DependsOn(
        typeof(CoreApplicationModule),
        typeof(CoreDomainTestModule)
        )]
    public class CoreApplicationTestModule : AbpModule
    {

    }
}