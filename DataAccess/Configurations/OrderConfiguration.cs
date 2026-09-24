namespace DataAccess.Configurations;

using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);
        builder.Property(o => o.TotalPrice).HasColumnType("numeric(18,2)");

        builder.HasOne(o => o.User)
               .WithMany()
               .HasForeignKey(o => o.UserId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(o => o.ShippingAddress)
               .WithMany()
               .HasForeignKey(o => o.ShippingAddressId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(o => o.BillingAddress)
               .WithMany()
               .HasForeignKey(o => o.BillingAddressId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}