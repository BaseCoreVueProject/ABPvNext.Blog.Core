using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Identity;
using Volo.Abp.Users;

namespace Bcvp.Blog.Core.BlogCore.Users
{
    public interface IBlogUserLookupService : IUserLookupService<BlogUser>
    {

    }
}
