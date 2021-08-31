using Bcvp.Blog.Core.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Bcvp.Blog.Core.Permissions
{
    public class CorePermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var bloggingGroup = context.AddGroup(CorePermissions.GroupName, L("Permission:Core"));

            var blogs = bloggingGroup.AddPermission(CorePermissions.Blogs.Default, L("Permission:Blogs"));
            blogs.AddChild(CorePermissions.Blogs.Management, L("Permission:Management"));
            blogs.AddChild(CorePermissions.Blogs.Update, L("Permission:Edit"));
            blogs.AddChild(CorePermissions.Blogs.Delete, L("Permission:Delete"));
            blogs.AddChild(CorePermissions.Blogs.Create, L("Permission:Create"));
            blogs.AddChild(CorePermissions.Blogs.ClearCache, L("Permission:ClearCache"));

            var posts = bloggingGroup.AddPermission(CorePermissions.Posts.Default, L("Permission:Posts"));
            posts.AddChild(CorePermissions.Posts.Update, L("Permission:Edit"));
            posts.AddChild(CorePermissions.Posts.Delete, L("Permission:Delete"));
            posts.AddChild(CorePermissions.Posts.Create, L("Permission:Create"));

            var tags = bloggingGroup.AddPermission(CorePermissions.Tags.Default, L("Permission:Tags"));
            tags.AddChild(CorePermissions.Tags.Update, L("Permission:Edit"));
            tags.AddChild(CorePermissions.Tags.Delete, L("Permission:Delete"));
            tags.AddChild(CorePermissions.Tags.Create, L("Permission:Create"));

            var comments = bloggingGroup.AddPermission(CorePermissions.Comments.Default, L("Permission:Comments"));
            comments.AddChild(CorePermissions.Comments.Update, L("Permission:Edit"));
            comments.AddChild(CorePermissions.Comments.Delete, L("Permission:Delete"));
            comments.AddChild(CorePermissions.Comments.Create, L("Permission:Create"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<CoreResource>(name);
        }
    }
}
