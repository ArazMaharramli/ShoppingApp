using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingApp.Domain.Models.Domain.AddressModels;

namespace ShoppingApp.Domain.Configurations.AddressModelsConfiguration
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.Property(x => x.ZipCode)
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(x => x.AddressLine1)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.AddressLine2)
                .HasMaxLength(200);

            builder.HasOne(x => x.City)
                .WithMany(c => c.Addresses);


            builder.HasIndex(x => x.UniqueId);

        }
    }
}
