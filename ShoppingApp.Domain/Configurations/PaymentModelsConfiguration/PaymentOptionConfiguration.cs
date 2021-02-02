using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingApp.Domain.Models.Domain.PaymentModels;

namespace ShoppingApp.Domain.Configurations.PaymentModelsConfiguration
{
    public class PaymentOptionConfiguration : IEntityTypeConfiguration<PaymentOption>
    {
        public void Configure(EntityTypeBuilder<PaymentOption> builder)
        {
            builder.Property(x => x.UniqueName)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.IconUrl)
                .HasMaxLength(2100);


            builder.HasIndex(x => x.UniqueId);

        }
    }
}
