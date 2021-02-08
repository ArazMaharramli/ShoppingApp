using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingApp.Domain.Models.Domain.ProductModels;

namespace ShoppingApp.Domain.Configurations.ProductModelsConfiguration
{
    public class SizeTypeConfiguration : IEntityTypeConfiguration<SizeType>
    {
        public void Configure(EntityTypeBuilder<SizeType> builder)
        {
            builder.Property(x => x.UniqueName)
                .IsRequired();

            builder.Property(x => x.Abbreviation)
                .HasMaxLength(7);


            builder.HasIndex(x => x.GlobalId);

        }
    }

}
