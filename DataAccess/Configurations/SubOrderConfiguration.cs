namespace DataAccess.Configurations;

using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class SubOrderConfiguration : IEntityTypeConfiguration<SubOrder>
{
    public void Configure(EntityTypeBuilder<SubOrder> builder)
    {
        builder.HasKey(so => so.Id);
        builder.Property(so => so.TotalPrice).HasColumnType("numeric(18,2)");

        builder.HasOne(so => so.Order)
               .WithMany(o => o.SubOrders)
               .HasForeignKey(so => so.OrderId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(so => so.Store)
               .WithMany(s => s.SubOrders)
               .HasForeignKey(so => so.StoreId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
