using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingApp.Domain.Models.Domain.ProductModels;

namespace ShoppingApp.Domain.Configurations.ProductModelsConfiguration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(x => x.UniqueName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.IconUrl)
                .HasMaxLength(2100);

            builder.Property(x => x.UniqueSlug)
                .IsRequired();

            builder.HasOne(x => x.Parent)
               .WithMany(c => c.Children);


            builder.HasIndex(x => x.GlobalId);

        }
    }

}
