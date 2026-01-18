using Api.KapiDunyasi.Domain.Policies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.KapiDunyasi.Infrastructure.Persistence.Configurations;

public class PolicyContentConfiguration : IEntityTypeConfiguration<PolicyContent>
{
    public void Configure(EntityTypeBuilder<PolicyContent> builder)
    {
        builder.ToTable("policy_contents");

        builder.HasKey(x => x.Key);
        builder.Property(x => x.Key).HasColumnName("key").IsRequired();
        builder.Property(x => x.TitleTR).HasColumnName("title_tr").IsRequired();
        builder.Property(x => x.BodyTR).HasColumnName("body_tr").IsRequired();

        builder.HasIndex(x => x.Key).IsUnique();
    }
}
