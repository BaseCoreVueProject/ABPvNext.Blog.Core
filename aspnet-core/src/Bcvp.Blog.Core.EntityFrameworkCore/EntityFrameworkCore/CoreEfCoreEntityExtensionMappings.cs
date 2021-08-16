using System;
using System.Linq;
using Bcvp.Blog.Core.BlogCore.Blogs;
using Bcvp.Blog.Core.BlogCore.Comments;
using Bcvp.Blog.Core.BlogCore.Posts;
using Bcvp.Blog.Core.BlogCore.Tagging;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.Identity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Threading;
using Volo.Abp.Users.EntityFrameworkCore;

namespace Bcvp.Blog.Core.EntityFrameworkCore
{
    public static class CoreEfCoreEntityExtensionMappings
    {
        private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

        public static void Configure()
        {
            CoreGlobalFeatureConfigurator.Configure();
            CoreModuleExtensionConfigurator.Configure();

            OneTimeRunner.Run(() =>
            {
                /* You can configure extra properties for the
                 * entities defined in the modules used by your application.
                 *
                 * This class can be used to map these extra properties to table fields in the database.
                 *
                 * USE THIS CLASS ONLY TO CONFIGURE EF CORE RELATED MAPPING.
                 * USE CoreModuleExtensionConfigurator CLASS (in the Domain.Shared project)
                 * FOR A HIGH LEVEL API TO DEFINE EXTRA PROPERTIES TO ENTITIES OF THE USED MODULES
                 *
                 * Example: Map a property to a table field:

                     ObjectExtensionManager.Instance
                         .MapEfCoreProperty<IdentityUser, string>(
                             "MyProperty",
                             (entityBuilder, propertyBuilder) =>
                             {
                                 propertyBuilder.HasMaxLength(128);
                             }
                         );

                 * See the documentation for more:
                 * https://docs.abp.io/en/abp/latest/Customizing-Application-Modules-Extending-Entities
                 */
            });
        }



        public static void ConfigureBcvpBlogCore([NotNull] this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            if (builder.IsTenantOnlyDatabase())
            {
                return;
            }


            builder.Entity<BlogCore.Blogs.Blog>(b =>
            {
                b.ToTable(CoreConsts.DbTablePrefix + "Blogs", CoreConsts.DbSchema);

                b.ConfigureByConvention();

                b.Property(x => x.Name).IsRequired().HasMaxLength(BlogConsts.MaxNameLength).HasColumnName(nameof(BlogCore.Blogs.Blog.Name));
                b.Property(x => x.ShortName).IsRequired().HasMaxLength(BlogConsts.MaxShortNameLength).HasColumnName(nameof(BlogCore.Blogs.Blog.ShortName));
                b.Property(x => x.Description).IsRequired(false).HasMaxLength(BlogConsts.MaxDescriptionLength).HasColumnName(nameof(BlogCore.Blogs.Blog.Description));

                b.ApplyObjectExtensionMappings();
            });

            builder.Entity<Post>(b =>
            {
                b.ToTable(CoreConsts.DbTablePrefix + "Posts", CoreConsts.DbSchema);

                b.ConfigureByConvention();

                b.Property(x => x.BlogId).HasColumnName(nameof(Post.BlogId));
                b.Property(x => x.Title).IsRequired().HasMaxLength(PostConsts.MaxTitleLength).HasColumnName(nameof(Post.Title));
                b.Property(x => x.CoverImage).IsRequired().HasColumnName(nameof(Post.CoverImage));
                b.Property(x => x.Url).IsRequired().HasMaxLength(PostConsts.MaxUrlLength).HasColumnName(nameof(Post.Url));
                b.Property(x => x.Content).IsRequired(false).HasMaxLength(PostConsts.MaxContentLength).HasColumnName(nameof(Post.Content));
                b.Property(x => x.Description).IsRequired(false).HasMaxLength(PostConsts.MaxDescriptionLength).HasColumnName(nameof(Post.Description));

                b.OwnsMany(p => p.Tags, pd =>
                {
                    pd.ToTable(CoreConsts.DbTablePrefix + "PostTags", CoreConsts.DbSchema);

                    pd.Property(x => x.TagId).HasColumnName(nameof(PostTag.TagId));

                    //    var properties = pd.OwnedEntityType.ClrType.GetProperties();
                    //    foreach (var property in properties)
                    //    {
                    //        var propertyType = property.PropertyType;

                    //        pd.Property(propertyType, property.Name).HasColumnName(property.Name);

                    //        b.Property(propertyType, property.Name);
                    //    }

                    //    pd.HasKey(properties.Select(x=>x.Name).ToArray());
                });
                
                b.ApplyObjectExtensionMappings();
            });

            builder.Entity<Tag>(b =>
            {
                b.ToTable(CoreConsts.DbTablePrefix + "Tags", CoreConsts.DbSchema);

                b.ConfigureByConvention();

                b.Property(x => x.Name).IsRequired().HasMaxLength(TagConsts.MaxNameLength).HasColumnName(nameof(Tag.Name));
                b.Property(x => x.Description).HasMaxLength(TagConsts.MaxDescriptionLength).HasColumnName(nameof(Tag.Description));
                b.Property(x => x.UsageCount).HasColumnName(nameof(Tag.UsageCount));

                b.ApplyObjectExtensionMappings();
            });


            builder.Entity<Comment>(b =>
            {
                b.ToTable(CoreConsts.DbTablePrefix + "Comments", CoreConsts.DbSchema);

                b.ConfigureByConvention();

                b.Property(x => x.Text).IsRequired().HasMaxLength(CommentConsts.MaxTextLength).HasColumnName(nameof(Comment.Text));
                b.Property(x => x.RepliedCommentId).HasColumnName(nameof(Comment.RepliedCommentId));
                b.Property(x => x.PostId).IsRequired().HasColumnName(nameof(Comment.PostId));

                b.HasOne<Comment>().WithMany().HasForeignKey(p => p.RepliedCommentId);
                b.HasOne<Post>().WithMany().IsRequired().HasForeignKey(p => p.PostId);

                b.ApplyObjectExtensionMappings();
            });


         

            builder.TryConfigureObjectExtensions<CoreDbContext>();

        }

    }
}
