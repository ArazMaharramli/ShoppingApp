using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingApp.Domain.Models.Domain.AdvertisementModels;

namespace ShoppingApp.Domain.Configurations.AdvertisementModelsConfiguration
{
    public class AdvertisementConfiguration : IEntityTypeConfiguration<Advertisement>
    {
        public void Configure(EntityTypeBuilder<Advertisement> builder)
        {
            builder.Property(x => x.PropmotionImageUrl)
                .HasMaxLength(2100)
                .IsRequired();


            builder.HasIndex(x => x.UniqueId);

        }
    }
}
