using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Bcvp.Blog.Core.Data
{
    /* This is used if database provider does't define
     * ICoreDbSchemaMigrator implementation.
     */
    public class NullCoreDbSchemaMigrator : ICoreDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}