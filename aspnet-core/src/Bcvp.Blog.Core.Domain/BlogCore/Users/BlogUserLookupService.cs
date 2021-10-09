using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Identity;
using Volo.Abp.Uow;
using Volo.Abp.Users;

namespace Bcvp.Blog.Core.BlogCore.Users
{
    public class BlogUserLookupService : UserLookupService<BlogUser, IBlogUserRepository>, IBlogUserLookupService
    {
        public BlogUserLookupService(
            IBlogUserRepository userRepository,
            IUnitOfWorkManager unitOfWorkManager)
            : base(
                userRepository,
                unitOfWorkManager)
        {

        }

        protected override BlogUser CreateUser(IUserData externalUser)
        {
            return new BlogUser(externalUser);
        }
    }
}
