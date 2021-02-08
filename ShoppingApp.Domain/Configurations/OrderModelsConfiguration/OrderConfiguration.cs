using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingApp.Domain.Models.Domain.OrderModels;

namespace ShoppingApp.Domain.Configurations.OrderModelsConfiguration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasOne(x => x.User)
                .WithMany(x => x.Orders);

            builder.HasOne(x => x.PaymentOption)
                .WithMany(x => x.Orders);

            builder.HasOne(x => x.DeliveryAddress)
                .WithMany(x => x.Orders);


            builder.HasIndex(x => x.GlobalId);

        }
    }
}
