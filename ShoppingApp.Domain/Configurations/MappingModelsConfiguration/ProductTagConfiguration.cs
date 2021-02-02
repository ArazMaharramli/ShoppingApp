using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingApp.Domain.Models.Domain.MappingModels;

namespace ShoppingApp.Domain.Configurations.MappingModelsConfiguration
{
    public class ProductTagConfiguration : IEntityTypeConfiguration<ProductTag>
    {
        public void Configure(EntityTypeBuilder<ProductTag> builder)
        {
            builder.HasOne(x => x.Product)
            .WithMany(x => x.ProductTags);

            builder.HasOne(x => x.Tag)
                .WithMany(x => x.ProductTags);

            builder.HasKey(x => new { x.ProductId, x.TagId });
        }
    }
}
