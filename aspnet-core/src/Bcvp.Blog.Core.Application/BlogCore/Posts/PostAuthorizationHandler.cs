using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Bcvp.Blog.Core.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Volo.Abp.Authorization.Permissions;

namespace Bcvp.Blog.Core.BlogCore.Posts
{
    public class PostAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, Post>
    {
        private readonly IPermissionChecker _permissionChecker;

        public PostAuthorizationHandler(IPermissionChecker permissionChecker)
        {
            _permissionChecker = permissionChecker;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement,
            Post resource)
        {

            if (requirement.Name == CommonOperations.Delete.Name && await HasDeletePermission(context, resource))
            {
                context.Succeed(requirement);
                return;
            }

            if (requirement.Name == CommonOperations.Update.Name && await HasUpdatePermission(context, resource))
            {
                context.Succeed(requirement);
                return;
            }

        }

        private async Task<bool> HasDeletePermission(AuthorizationHandlerContext context, Post resource)
        {
            if (resource.CreatorId != null && resource.CreatorId == context.User.FindUserId())
            {
                return true;
            }

            if (await _permissionChecker.IsGrantedAsync(context.User, CorePermissions.Posts.Delete))
            {
                return true;
            }

            return false;
        }

        private async Task<bool> HasUpdatePermission(AuthorizationHandlerContext context, Post resource)
        {
            if (resource.CreatorId != null && resource.CreatorId == context.User.FindUserId())
            {
                return true;
            }

            if (await _permissionChecker.IsGrantedAsync(context.User, CorePermissions.Posts.Update))
            {
                return true;
            }

            return false;
        }
    }
}
