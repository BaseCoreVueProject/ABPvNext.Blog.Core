using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Bcvp.Blog.Core.BlogCore.Comments
{
    public class Comment : FullAuditedAggregateRoot<Guid>
    {
        public virtual Guid PostId { get; protected set; }

        public virtual Guid? RepliedCommentId { get; protected set; }

        public virtual string Text { get; protected set; }

        protected Comment()
        {

        }

        public Comment(Guid id, Guid postId, Guid? repliedCommentId, [NotNull] string text)
        {
            Id = id;
            PostId = postId;
            RepliedCommentId = repliedCommentId;
            Text = Check.NotNullOrWhiteSpace(text, nameof(text));
        }

        public void SetText(string text)
        {
            Text = Check.NotNullOrWhiteSpace(text, nameof(text));
        }
    }
}
