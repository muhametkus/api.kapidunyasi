using Api.KapiDunyasi.Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.KapiDunyasi.Infrastructure.Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("orders");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.OrderNo).HasColumnName("order_no").IsRequired();
        builder.Property(x => x.Status).HasColumnName("status").IsRequired();

        builder.Property(x => x.FormName).HasColumnName("form_name").IsRequired();
        builder.Property(x => x.Phone).HasColumnName("phone").IsRequired();
        builder.Property(x => x.Email).HasColumnName("email").IsRequired();
        builder.Property(x => x.Address).HasColumnName("address");
        builder.Property(x => x.InvoiceType).HasColumnName("invoice_type");
        builder.Property(x => x.Payment).HasColumnName("payment");

        builder.Property(x => x.CreatedAt).HasColumnName("created_at");
        builder.Property(x => x.UpdatedAt).HasColumnName("updated_at");

        builder.HasIndex(x => x.OrderNo).IsUnique();
        builder.HasIndex(x => x.Status);
        builder.HasIndex(x => x.CreatedAt);

        builder.OwnsMany(x => x.Items, items =>
        {
            items.ToTable("order_items");
            items.WithOwner().HasForeignKey("order_id");

            items.HasKey(x => x.Id);
            items.Property(x => x.Id).HasColumnName("id");
            items.Property(x => x.ProductId).HasColumnName("product_id");
            items.Property(x => x.NameTR).HasColumnName("name_tr").IsRequired();
            items.Property(x => x.Price).HasColumnName("price").HasColumnType("numeric(12,2)");
            items.Property(x => x.Image).HasColumnName("image");
            items.Property(x => x.Qty).HasColumnName("qty").IsRequired();

            items.Property(x => x.VariantSize).HasColumnName("variant_size");
            items.Property(x => x.VariantFrame).HasColumnName("variant_frame");
            items.Property(x => x.VariantDirection).HasColumnName("variant_direction");
            items.Property(x => x.VariantColor).HasColumnName("variant_color");
            items.Property(x => x.VariantThickness).HasColumnName("variant_thickness");

            items.Property(x => x.CreatedAt).HasColumnName("created_at");
            items.Property(x => x.UpdatedAt).HasColumnName("updated_at");

            items.HasIndex(x => x.ProductId);
        });
    }
}
