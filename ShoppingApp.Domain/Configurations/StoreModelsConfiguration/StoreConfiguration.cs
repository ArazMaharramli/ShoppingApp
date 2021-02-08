using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingApp.Domain.Models.Domain.StoreModels;

namespace ShoppingApp.Domain.Configurations.StoreModelsConfiguration
{
    public class StoreConfiguration : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            builder.Property(x => x.StoreName)
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(x => x.UniqueSlug)
                .IsRequired();

            builder.HasOne(x => x.StoreType)
                .WithMany(x => x.Stores);

            builder.HasOne(x => x.Address)
                .WithMany(x => x.Stores);


            builder.HasIndex(x => x.GlobalId);

        }
    }

    
}
