using Volo.Abp.Identity.Settings;
using Volo.Abp.Settings;

namespace Bcvp.Blog.Core.Settings
{
    public class CoreSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(CoreSettings.MySetting1));

        }
    }
}
