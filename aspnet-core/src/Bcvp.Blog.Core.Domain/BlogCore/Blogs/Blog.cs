using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Bcvp.Blog.Core.BlogCore.Blogs
{
    public class Blog:FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string Name { get; protected set; }

        [NotNull]
        public virtual string ShortName { get; protected set; }

        [CanBeNull]
        public virtual string Description { get; set; }

        protected Blog()
        {

        }

        public Blog(Guid id, [NotNull] string name, [NotNull] string shortName)
        {
            //属性赋值
            Id = id;
            //有效性检测
            Name = Check.NotNullOrWhiteSpace(name, nameof(name));
            //有效性检测
            ShortName = Check.NotNullOrWhiteSpace(shortName, nameof(shortName));
        }

        public virtual Blog SetName([NotNull] string name)
        {
            Name = Check.NotNullOrWhiteSpace(name, nameof(name));
            return this;
        }

        public virtual Blog SetShortName(string shortName)
        {
            ShortName = Check.NotNullOrWhiteSpace(shortName, nameof(shortName));
            return this;
        }

    }
}
