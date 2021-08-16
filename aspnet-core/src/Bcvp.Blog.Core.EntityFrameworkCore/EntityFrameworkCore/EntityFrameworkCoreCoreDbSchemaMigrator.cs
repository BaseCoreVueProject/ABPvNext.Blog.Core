using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Bcvp.Blog.Core.Data;
using Volo.Abp.DependencyInjection;

namespace Bcvp.Blog.Core.EntityFrameworkCore
{
    public class EntityFrameworkCoreCoreDbSchemaMigrator
        : ICoreDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreCoreDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the CoreDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<CoreDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}
