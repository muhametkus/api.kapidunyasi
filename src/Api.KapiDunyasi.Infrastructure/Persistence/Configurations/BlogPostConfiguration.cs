using Api.KapiDunyasi.Domain.Blog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.KapiDunyasi.Infrastructure.Persistence.Configurations;

public class BlogPostConfiguration : IEntityTypeConfiguration<BlogPost>
{
    public void Configure(EntityTypeBuilder<BlogPost> builder)
    {
        builder.ToTable("blog_posts");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.TitleTR).HasColumnName("title_tr").IsRequired();
        builder.Property(x => x.Slug).HasColumnName("slug").IsRequired();
        builder.Property(x => x.ExcerptTR).HasColumnName("excerpt_tr");
        builder.Property(x => x.PublishedAt).HasColumnName("published_at");
        builder.Property(x => x.CreatedAt).HasColumnName("created_at");
        builder.Property(x => x.UpdatedAt).HasColumnName("updated_at");

        builder.Property(x => x.TagsTR)
            .HasColumnName("tags_tr")
            .HasColumnType("text[]")
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.HasIndex(x => x.Slug).IsUnique();
        builder.HasIndex(x => x.PublishedAt);
        builder.HasIndex(x => x.TagsTR).HasMethod("gin");

        builder.OwnsMany(x => x.ContentTR, content =>
        {
            content.ToTable("blog_content_sections");
            content.WithOwner().HasForeignKey("blog_post_id");

            content.Property<Guid>("Id").HasColumnName("id");
            content.HasKey("Id");

            content.Property(x => x.HeadingTR).HasColumnName("heading_tr").IsRequired();
            content.Property(x => x.BodyTR).HasColumnName("body_tr").IsRequired();
        });
    }
}
