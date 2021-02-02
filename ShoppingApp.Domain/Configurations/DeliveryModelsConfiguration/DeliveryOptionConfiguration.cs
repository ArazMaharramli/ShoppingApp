using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingApp.Domain.Models.Domain.DeliveryModels;

namespace ShoppingApp.Domain.Configurations.DeliveryModelsConfiguration
{
    public class DeliveryOptionConfiguration : IEntityTypeConfiguration<DeliveryOption>
    {
        public void Configure(EntityTypeBuilder<DeliveryOption> builder)
        {
            builder.Property(x => x.UniqueName)
                .HasMaxLength(25)
                .IsRequired();

            builder.Property(x => x.Description)
                .IsRequired();


            builder.HasIndex(x => x.UniqueId);

        }
    }
}
