using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingApp.Domain.Models.Domain.MappingModels;

namespace ShoppingApp.Domain.Configurations.MappingModelsConfiguration
{
    public class StoreDeliveryOptionConfiguration : IEntityTypeConfiguration<StoreDeliveryOption>
    {
        public void Configure(EntityTypeBuilder<StoreDeliveryOption> builder)
        {
            builder.HasOne(x => x.Store)
                .WithMany(x => x.StoreDeliveryOptions);

            builder.HasOne(x => x.DeliveryOption)
                .WithMany(x => x.StoreDeliveryOptions);

            builder.HasKey(x => new { x.DeliveryOptionId, x.StoreId });
        }
    }
}
