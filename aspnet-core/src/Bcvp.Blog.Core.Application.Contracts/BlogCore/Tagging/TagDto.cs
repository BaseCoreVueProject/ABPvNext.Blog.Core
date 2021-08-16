using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Bcvp.Blog.Core.BlogCore.Tagging
{
    public class TagDto : FullAuditedEntityDto<Guid>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int UsageCount { get; set; }
    }
}
