using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingApp.Domain.Models.Domain.OrderModels;

namespace ShoppingApp.Domain.Configurations.OrderModelsConfiguration
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property(x => x.Quantity)
                .IsRequired();

            builder.HasOne(x => x.Order)
                .WithMany(x => x.OrderItems);

            builder.HasOne(x => x.ProductDetail)
                .WithMany(x => x.OrderItems)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.DeliveryOption)
                .WithMany(x => x.OrderItems);


            builder.HasIndex(x => x.GlobalId);

        }
    }
}
