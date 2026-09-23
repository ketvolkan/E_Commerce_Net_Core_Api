namespace DataAccess.Configurations;

using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ProductVariantConfiguration : IEntityTypeConfiguration<ProductVariant>
{
    public void Configure(EntityTypeBuilder<ProductVariant> builder)
    {
        builder.HasKey(pv => pv.Id);

        builder.Property(pv => pv.Price).HasColumnType("numeric(18,2)");
        builder.Property(pv => pv.DiscountPrice).HasColumnType("numeric(18,2)");

        builder.HasOne(pv => pv.Product)
               .WithMany(p => p.ProductVariants)
               .HasForeignKey(pv => pv.ProductId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(pv => pv.Store)
               .WithMany(s => s.ProductVariants)
               .HasForeignKey(pv => pv.StoreId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}