using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingApp.Domain.Models.Domain.ProductModels;

namespace ShoppingApp.Domain.Configurations.ProductModelsConfiguration
{
    public class ColorConfiguration : IEntityTypeConfiguration<Color>
    {
        public void Configure(EntityTypeBuilder<Color> builder)
        {
            builder.Property(x => x.UniqueTitle)
                .IsRequired();

            builder.Property(x => x.HexCode)
                .HasMaxLength(9);


            builder.HasIndex(x => x.GlobalId);

        }
    }

}
