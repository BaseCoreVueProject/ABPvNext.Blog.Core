using AutoMapper;
using Bcvp.Blog.Core.BlogCore.Blogs;
using Bcvp.Blog.Core.BlogCore.Comments;
using Bcvp.Blog.Core.BlogCore.Posts;
using Bcvp.Blog.Core.BlogCore.Tagging;
using Volo.Abp.AutoMapper;
using Volo.Abp.Identity;

namespace Bcvp.Blog.Core
{
    public class CoreApplicationAutoMapperProfile : Profile
    {
        public CoreApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */

            CreateMap<Comment, CommentWithDetailsDto>().Ignore(x => x.Writer);


            CreateMap<BlogCore.Blogs.Blog, BlogDto>();
            CreateMap<IdentityUser, BlogUserDto>();

            CreateMap<Post, PostWithDetailsDto>().Ignore(x => x.Writer).Ignore(x => x.CommentCount).Ignore(x => x.Tags);

            CreateMap<Tag, TagDto>();

            CreateMap<Post, PostCacheItem>().Ignore(x => x.CommentCount).Ignore(x => x.Tags);
            CreateMap<PostCacheItem, PostWithDetailsDto>()
                .IgnoreModificationAuditedObjectProperties()
                .IgnoreDeletionAuditedObjectProperties()
                .Ignore(x => x.Writer)
                .Ignore(x => x.CommentCount)
                .Ignore(x => x.Tags);


         



        }
    }
}
