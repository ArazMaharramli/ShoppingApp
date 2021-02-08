using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingApp.Domain.Models.Domain.AddressModels;

namespace ShoppingApp.Domain.Configurations.AddressModelsConfiguration
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.Property(x => x.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Abbreviation)
                .HasMaxLength(5)
                .IsRequired();

            builder.Property(x => x.PhoneNumberPrefix)
                .HasMaxLength(5)
                .IsRequired();


            builder.HasIndex(x => x.GlobalId);

        }
    }
}
