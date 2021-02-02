using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingApp.Domain.Models.Domain.ProductModels;

namespace ShoppingApp.Domain.Configurations.ProductModelsConfiguration
{
    public class SizeConfiguration : IEntityTypeConfiguration<Size>
    {
        public void Configure(EntityTypeBuilder<Size> builder)
        {
            builder.Property(x => x.UniqueTitle)
                 .IsRequired();

            builder.HasOne(x => x.SizeType)
                .WithMany(x => x.Sizes);


            builder.HasIndex(x => x.UniqueId);

        }
    }

}
