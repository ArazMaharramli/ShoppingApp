using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingApp.Domain.Models.Domain.ProductModels;

namespace ShoppingApp.Domain.Configurations.ProductModelsConfiguration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Title)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.UniqueSlug)
                .IsRequired();

            builder.Property(x => x.ShortDescription)
                .IsRequired();

            builder.Property(x => x.Description)
                .IsRequired();

            builder.HasOne(x => x.Brand)
                .WithMany(X => X.Products);

            builder.HasOne(x => x.Material)
                .WithMany(x => x.Products);

            builder.HasOne(x => x.Store)
               .WithMany(x => x.Products);

            builder.HasIndex(x => x.GlobalId);

        }
    }

}
