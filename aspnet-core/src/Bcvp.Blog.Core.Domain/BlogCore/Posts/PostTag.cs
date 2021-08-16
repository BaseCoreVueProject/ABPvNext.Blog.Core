using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Domain.Values;

namespace Bcvp.Blog.Core.BlogCore.Posts
{
    public record PostTag 
    {
         public virtual Guid TagId { get; init; }  //主键

        protected PostTag()
        {

        }

        public PostTag(Guid tagId)
        {
            TagId = tagId;
        }
    }
}
