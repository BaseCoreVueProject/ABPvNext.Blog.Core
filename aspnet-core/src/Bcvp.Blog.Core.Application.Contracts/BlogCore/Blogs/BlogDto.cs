using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Bcvp.Blog.Core.BlogCore.Blogs
{
    public class BlogDto : FullAuditedEntityDto<Guid>
    {
        public string Name { get; set; }

        public string ShortName { get; set; }

        public string Description { get; set; }
    }
}
