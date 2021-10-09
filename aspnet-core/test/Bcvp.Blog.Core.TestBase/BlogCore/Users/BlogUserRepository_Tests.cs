using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shouldly;
using Volo.Abp.Modularity;
using Xunit;

namespace Bcvp.Blog.Core.BlogCore.Users
{
    public abstract class BlogUserRepository_Tests<TStartupModule> : CoreTestBase<TStartupModule>
        where TStartupModule : IAbpModule
    {

        protected IBlogUserRepository UserRepository { get; }

        protected BlogUserRepository_Tests()
        {
            UserRepository = GetRequiredService<IBlogUserRepository>();
        }

        [Fact]
        public async Task GetUsersAsync()
        {
            var users = await UserRepository.GetUsersAsync(10, null);
            users.ShouldNotBeNull();
            users.Count.ShouldBe(0);
        }


    }
}
