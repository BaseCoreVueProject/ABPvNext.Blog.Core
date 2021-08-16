using System.Threading.Tasks;

namespace Bcvp.Blog.Core.Data
{
    public interface ICoreDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
