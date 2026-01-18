using Api.KapiDunyasi.Domain.References;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.KapiDunyasi.Infrastructure.Persistence.Configurations;

public class ReferenceConfiguration : IEntityTypeConfiguration<Reference>
{
    public void Configure(EntityTypeBuilder<Reference> builder)
    {
        builder.ToTable("references");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.ProjectNameTR).HasColumnName("project_name_tr").IsRequired();
        builder.Property(x => x.CityTR).HasColumnName("city_tr");
        builder.Property(x => x.Year).HasColumnName("year");
        builder.Property(x => x.TypeTR).HasColumnName("type_tr");
        builder.Property(x => x.DescriptionTR).HasColumnName("description_tr");
        builder.Property(x => x.CreatedAt).HasColumnName("created_at");
        builder.Property(x => x.UpdatedAt).HasColumnName("updated_at");

        builder.HasIndex(x => x.Year);
        builder.HasIndex(x => x.CityTR);
    }
}
