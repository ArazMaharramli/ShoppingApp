using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingApp.Domain.Models.Domain.ProductModels;

namespace ShoppingApp.Domain.Configurations.ProductModelsConfiguration
{
    public class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.Property(x => x.UniqueName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.LogoUrl)
                .HasMaxLength(2100);

            builder.Property(x => x.UniqueSlug)
                .IsRequired();

            builder.HasIndex(x => x.GlobalId);

        }
    }

}
