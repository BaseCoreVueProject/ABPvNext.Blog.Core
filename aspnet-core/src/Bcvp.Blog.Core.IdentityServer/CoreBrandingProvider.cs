using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Bcvp.Blog.Core
{
    [Dependency(ReplaceServices = true)]
    public class CoreBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "Core";
    }
}
