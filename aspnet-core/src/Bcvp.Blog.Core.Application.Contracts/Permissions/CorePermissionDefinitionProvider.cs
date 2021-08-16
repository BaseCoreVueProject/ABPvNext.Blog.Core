using Bcvp.Blog.Core.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Bcvp.Blog.Core.Permissions
{
    public class CorePermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(CorePermissions.GroupName);

            //Define your own permissions here. Example:
            //myGroup.AddPermission(CorePermissions.MyPermission1, L("Permission:MyPermission1"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<CoreResource>(name);
        }
    }
}
