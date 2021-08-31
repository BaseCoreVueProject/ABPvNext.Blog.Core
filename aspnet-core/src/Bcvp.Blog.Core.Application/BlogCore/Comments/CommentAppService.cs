using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bcvp.Blog.Core.BlogCore.Posts;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using Volo.Abp.Users;

namespace Bcvp.Blog.Core.BlogCore.Comments
{
    public class CommentAppService : CoreAppService, ICommentAppService
    {
        private IUserLookupService<IdentityUser> UserLookupService { get; }

        private readonly ICommentRepository _commentRepository;
        private readonly IGuidGenerator _guidGenerator;

        public CommentAppService(ICommentRepository commentRepository, IGuidGenerator guidGenerator, IUserLookupService<IdentityUser> userLookupService)
        {
            _commentRepository = commentRepository;
            _guidGenerator = guidGenerator;
            UserLookupService = userLookupService;
        }

        public async Task<List<CommentWithRepliesDto>> GetHierarchicalListOfPostAsync(Guid postId)
        {
            // 获取评论数据
            var comments = await GetListOfPostAsync(postId);

            #region 对评论的作者进行赋值

            var userDictionary = new Dictionary<Guid, BlogUserDto>();

            foreach (var commentDto in comments)
            {
                if (commentDto.CreatorId.HasValue)
                {
                    var creatorUser = await UserLookupService.FindByIdAsync(commentDto.CreatorId.Value);

                    if (creatorUser != null && !userDictionary.ContainsKey(creatorUser.Id))
                    {
                        userDictionary.Add(creatorUser.Id, ObjectMapper.Map<IdentityUser, BlogUserDto>(creatorUser));
                    }
                }
            }

            foreach (var commentDto in comments)
            {
                if (commentDto.CreatorId.HasValue && userDictionary.ContainsKey((Guid)commentDto.CreatorId))
                {
                    commentDto.Writer = userDictionary[(Guid)commentDto.CreatorId];
                }
            }

            #endregion

            var hierarchicalComments = new List<CommentWithRepliesDto>();

            #region 包装评论数据格式

            // 评论包装成2级（ps:前面的查询根据时间排序，这里不要担心子集在父级前面）

            foreach (var commentDto in comments)
            {
                var parent = hierarchicalComments.Find(c => c.Comment.Id == commentDto.RepliedCommentId);

                if (parent != null)
                {
                    parent.Replies.Add(commentDto);
                }
                else
                {
                    hierarchicalComments.Add(new CommentWithRepliesDto() { Comment = commentDto });
                }
            }

            hierarchicalComments = hierarchicalComments.OrderByDescending(c => c.Comment.CreationTime).ToList();


            #endregion
          

            return hierarchicalComments;

        }

        public async Task<CommentWithDetailsDto> CreateAsync(CreateCommentDto input)
        {
            // 也可以使用这种方式(这里只是介绍用法) GuidGenerator.Create()
            var comment = new Comment(_guidGenerator.Create(), input.PostId, input.RepliedCommentId, input.Text);

            comment = await _commentRepository.InsertAsync(comment);

            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<Comment, CommentWithDetailsDto>(comment);
        }

        public async Task<CommentWithDetailsDto> UpdateAsync(Guid id, UpdateCommentDto input)
        {
            var comment = await _commentRepository.GetAsync(id);

            comment.SetText(input.Text);

            comment = await _commentRepository.UpdateAsync(comment);

            return ObjectMapper.Map<Comment, CommentWithDetailsDto>(comment);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _commentRepository.DeleteAsync(id);

            var replies = await _commentRepository.GetRepliesOfComment(id);

            foreach (var reply in replies)
            {
                await _commentRepository.DeleteAsync(reply.Id);
            }
        }


        private async Task<List<CommentWithDetailsDto>> GetListOfPostAsync(Guid postId)
        {
            var comments = await _commentRepository.GetListOfPostAsync(postId);

            return new List<CommentWithDetailsDto>(
                ObjectMapper.Map<List<Comment>, List<CommentWithDetailsDto>>(comments));
        }

    }
}
