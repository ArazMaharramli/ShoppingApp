using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingApp.Domain.Models.Domain.MappingModels;

namespace ShoppingApp.Domain.Configurations.MappingModelsConfiguration
{
    public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.HasOne(x => x.Product)
           .WithMany(x => x.ProductCategories);

            builder.HasOne(x => x.Category)
                .WithMany(x => x.ProductCategories);

            builder.HasKey(x => new { x.CategoryId, x.ProductId });
        }
    }
}
