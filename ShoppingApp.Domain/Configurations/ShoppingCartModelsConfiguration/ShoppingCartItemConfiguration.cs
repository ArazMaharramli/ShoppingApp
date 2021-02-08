using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingApp.Domain.Models.Domain.ShoppingCartModels;

namespace ShoppingApp.Domain.Configurations.ShoppingCartModelsConfiguration
{
    public class ShoppingCartItemConfiguration : IEntityTypeConfiguration<ShoppingCartItem>
    {
        public void Configure(EntityTypeBuilder<ShoppingCartItem> builder)
        {
            builder.Property(x => x.Quantity)
                .IsRequired();

            builder.HasOne(x => x.Product)
                .WithMany(x => x.ShoppingCartItems);

            builder.HasOne(x => x.ShoppingCart)
                .WithMany(x => x.ShoppingCartItems);


            builder.HasIndex(x => x.GlobalId);


        }
    }
}
