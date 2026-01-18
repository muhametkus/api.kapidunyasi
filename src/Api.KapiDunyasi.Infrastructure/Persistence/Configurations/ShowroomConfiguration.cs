using Api.KapiDunyasi.Domain.Showrooms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.KapiDunyasi.Infrastructure.Persistence.Configurations;

public class ShowroomConfiguration : IEntityTypeConfiguration<Showroom>
{
    public void Configure(EntityTypeBuilder<Showroom> builder)
    {
        builder.ToTable("showrooms");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.NameTR).HasColumnName("name_tr").IsRequired();
        builder.Property(x => x.Slug).HasColumnName("slug").IsRequired();
        builder.Property(x => x.Phone).HasColumnName("phone");
        builder.Property(x => x.AddressTR).HasColumnName("address_tr");
        builder.Property(x => x.MapEmbedUrl).HasColumnName("map_embed_url");
        builder.Property(x => x.WorkingHoursTR).HasColumnName("working_hours_tr");
        builder.Property(x => x.CreatedAt).HasColumnName("created_at");
        builder.Property(x => x.UpdatedAt).HasColumnName("updated_at");

        builder.HasIndex(x => x.Slug).IsUnique();
    }
}
