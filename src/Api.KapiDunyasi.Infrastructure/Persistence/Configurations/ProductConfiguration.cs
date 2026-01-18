using Api.KapiDunyasi.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.KapiDunyasi.Infrastructure.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("products");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.NameTR).HasColumnName("name_tr").IsRequired();
        builder.Property(x => x.Slug).HasColumnName("slug").IsRequired();
        builder.Property(x => x.SeriesTR).HasColumnName("series_tr");
        builder.Property(x => x.CategoryId).HasColumnName("category_id").IsRequired();
        builder.Property(x => x.StockStatus).HasColumnName("stock_status").IsRequired();
        builder.Property(x => x.StockCount).HasColumnName("stock_count");
        builder.Property(x => x.Code).HasColumnName("code");
        builder.Property(x => x.FireResistant).HasColumnName("fire_resistant").HasDefaultValue(false);
        builder.Property(x => x.WarrantyTR).HasColumnName("warranty_tr");
        builder.Property(x => x.ShippingTR).HasColumnName("shipping_tr");
        builder.Property(x => x.CreatedAt).HasColumnName("created_at");
        builder.Property(x => x.UpdatedAt).HasColumnName("updated_at");

        builder.Property(x => x.TagsTR)
            .HasColumnName("tags_tr")
            .HasColumnType("text[]")
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.Property(x => x.BadgesTR)
            .HasColumnName("badges_tr")
            .HasColumnType("text[]")
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.Property(x => x.Images)
            .HasColumnName("images")
            .HasColumnType("text[]")
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.OwnsOne(x => x.Price, price =>
        {
            price.Property(p => p.Type).HasColumnName("price_type").IsRequired();
            price.Property(p => p.Value).HasColumnName("price_value").HasColumnType("numeric(12,2)");
            price.Property(p => p.Min).HasColumnName("price_min").HasColumnType("numeric(12,2)");
            price.Property(p => p.Max).HasColumnName("price_max").HasColumnType("numeric(12,2)");
            price.HasIndex(p => new { p.Min, p.Max });
        });

        builder.OwnsOne(x => x.Specs, specs =>
        {
            specs.Property(s => s.MaterialTR).HasColumnName("specs_material_tr");
            specs.Property(s => s.SurfaceTR).HasColumnName("specs_surface_tr");

            specs.Property(s => s.ThicknessOptions)
                .HasColumnName("specs_thickness_options")
                .HasColumnType("text[]")
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            specs.Property(s => s.FrameOptions)
                .HasColumnName("specs_frame_options")
                .HasColumnType("text[]")
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            specs.Property(s => s.Sizes)
                .HasColumnName("specs_sizes")
                .HasColumnType("text[]")
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            specs.Property(s => s.ColorOptionsTR)
                .HasColumnName("specs_color_options_tr")
                .HasColumnType("text[]")
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            specs.Property(s => s.OpeningDirectionsTR)
                .HasColumnName("specs_opening_directions_tr")
                .HasColumnType("text[]")
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        });

        builder.HasIndex(x => x.Slug).IsUnique();
        builder.HasIndex(x => x.CategoryId);
        builder.HasIndex(x => x.SeriesTR);
        builder.HasIndex(x => x.StockStatus);
        builder.HasIndex(x => x.FireResistant);
        builder.HasIndex(x => x.TagsTR).HasMethod("gin");
        builder.HasIndex(x => x.BadgesTR).HasMethod("gin");
    }
}
