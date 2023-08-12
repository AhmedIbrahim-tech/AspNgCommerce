using ECommerce.Core.Entities.OrderAggregate;

namespace ECommerce.Infrastrucure.Data.Configurations;

public class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
{
    public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
    {
        builder.Property(d => d.Price)
            .HasColumnType("decimal(18,2)");



        builder.HasData(
                new DeliveryMethod
                {
                    Id = 1,
                    ShortName = "UPS1",
                    DeliveryTime = "1-2 Days",
                    Description = "Fastest delivery time",
                    Price = 10
                }, new DeliveryMethod
                {
                    Id = 2,
                    ShortName = "UPS2",
                    DeliveryTime = "2-5 Days",
                    Description = "Get it within 5 days",
                    Price = 5
                },
                new DeliveryMethod
                {
                    Id = 1,
                    ShortName = "UPS3",
                    DeliveryTime = "5-10 Days",
                    Description = "Slower but cheap",
                    Price = 2
                },
                new DeliveryMethod
                {
                    Id = 1,
                    ShortName = "FREE",
                    DeliveryTime = "Free! You get what you pay for",
                    Description = "1-2 Weeks",
                    Price = 0
                }
            );
    }
}
