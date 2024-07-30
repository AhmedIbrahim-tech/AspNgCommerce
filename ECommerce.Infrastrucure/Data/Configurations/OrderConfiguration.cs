using ECommerce.Core.Entities.OrderAggregate;
using Order = ECommerce.Core.Entities.OrderAggregate.Order;

namespace ECommerce.Infrastrucure.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.OwnsOne(o => o.ShipToAddress, a =>
        {
            a.WithOwner();
        });

        builder.Navigation(a => a.ShipToAddress).IsRequired();

        builder.Property(a => a.Status)
            .HasConversion(
             o => o.ToString(),
             o => (OrderStatus)Enum.Parse(typeof(OrderStatus), o)
             );

        builder.HasMany(o => o.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade);

    }
}
