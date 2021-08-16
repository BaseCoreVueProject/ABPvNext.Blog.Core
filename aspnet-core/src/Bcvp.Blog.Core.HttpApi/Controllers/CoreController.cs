using Bcvp.Blog.Core.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Bcvp.Blog.Core.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class CoreController : AbpController
    {
        protected CoreController()
        {
            LocalizationResource = typeof(CoreResource);
        }
    }
}