using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingApp.Domain.Models.Domain.MediaModels;

namespace ShoppingApp.Domain.Configurations.MediaModelsConfiguration
{
    public class ProductMediaConfiguration : IEntityTypeConfiguration<ProductMedia>
    {
        public void Configure(EntityTypeBuilder<ProductMedia> builder)
        {
            builder.Property(x => x.MediaUrl)
                .HasMaxLength(2100)
                .IsRequired();
    
            builder.Property(x => x.MediaType)
                .IsRequired();
    
            builder.HasOne(x => x.Product)
                .WithMany(x => x.ProductMedias);


            builder.HasIndex(x => x.GlobalId);

        }
    }
}
