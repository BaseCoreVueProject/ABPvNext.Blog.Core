using Bcvp.Blog.Core.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace Bcvp.Blog.Core.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(CoreEntityFrameworkCoreModule),
        typeof(CoreApplicationContractsModule)
        )]
    public class CoreDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
