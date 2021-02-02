using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingApp.Domain.Models.Domain.AddressModels;

namespace ShoppingApp.Domain.Configurations.AddressModelsConfiguration
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.Property(x => x.Name)
                .HasMaxLength(30)
                .IsRequired();

            builder.HasOne(x => x.Country)
                .WithMany(c => c.Cities);

            builder.HasIndex(x => x.UniqueId);

        }
    }
}
