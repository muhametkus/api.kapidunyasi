using Api.KapiDunyasi.Domain.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.KapiDunyasi.Infrastructure.Persistence.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("categories");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.NameTR).HasColumnName("name_tr").IsRequired();
        builder.Property(x => x.Slug).HasColumnName("slug").IsRequired();
        builder.Property(x => x.DescriptionTR).HasColumnName("description_tr");
        builder.Property(x => x.ParentId).HasColumnName("parent_id");
        builder.Property(x => x.CreatedAt).HasColumnName("created_at");
        builder.Property(x => x.UpdatedAt).HasColumnName("updated_at");

        builder.HasIndex(x => x.Slug).IsUnique();
        builder.HasIndex(x => x.ParentId);

        builder.HasOne(x => x.Parent)
            .WithMany(x => x.Children)
            .HasForeignKey(x => x.ParentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
