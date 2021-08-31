using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Bcvp.Blog.Core.BlogCore.Comments
{
    public interface ICommentRepository : IBasicRepository<Comment, Guid>
    {
        /// <summary>
        /// 根据文章Id 获取评论
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<Comment>> GetListOfPostAsync(Guid postId, CancellationToken cancellationToken = default);
        /// <summary>
        /// 根据文章Id 获取评论数量
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> GetCommentCountOfPostAsync(Guid postId, CancellationToken cancellationToken = default);
        /// <summary>
        /// 根据评论Id 下面的子获取评论
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<Comment>> GetRepliesOfComment(Guid id, CancellationToken cancellationToken = default);

        Task DeleteOfPost(Guid id, CancellationToken cancellationToken = default);
    }
}
