using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingApp.Domain.Models.Domain.ShoppingCartModels;

namespace ShoppingApp.Domain.Configurations.ShoppingCartModelsConfiguration
{
    public class ShoppingCartConfiguration : IEntityTypeConfiguration<ShoppingCart>
    {
        public void Configure(EntityTypeBuilder<ShoppingCart> builder)
        {
            builder.Property(x => x.ShoppingCardType)
                .IsRequired();

            builder.HasOne(x => x.User)
                .WithMany(x => x.ShoppingCarts);


            builder.HasIndex(x => x.UniqueId);

        }
    }
}
