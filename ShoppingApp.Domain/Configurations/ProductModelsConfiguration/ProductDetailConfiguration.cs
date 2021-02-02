using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingApp.Domain.Models.Domain.ProductModels;

namespace ShoppingApp.Domain.Configurations.ProductModelsConfiguration
{
    public class ProductDetailConfiguration : IEntityTypeConfiguration<ProductDetail>
    {
        public void Configure(EntityTypeBuilder<ProductDetail> builder)
        {
            builder.Property(x => x.StockQuantity)
                .IsRequired();

            builder.HasOne(x => x.Product)
                .WithMany(x => x.ProductDetails);

            builder.HasOne(x => x.Color)
                .WithMany(x => x.ProductDetails);

            builder.HasOne(x => x.Size)
                .WithMany(x => x.ProductDetails);


            builder.HasIndex(x => x.UniqueId);

        }
    }

}
