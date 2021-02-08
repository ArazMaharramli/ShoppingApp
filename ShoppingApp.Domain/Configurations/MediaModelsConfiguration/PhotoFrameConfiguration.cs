using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingApp.Domain.Models.Domain.MediaModels;

namespace ShoppingApp.Domain.Configurations.MediaModelsConfiguration
{
    public partial class PhotoFrameConfiguration : IEntityTypeConfiguration<PhotoFrame>
    {
        public void Configure(EntityTypeBuilder<PhotoFrame> builder)
        {
            builder.Property(x => x.FrameName)
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(x => x.FrameUrl)
                .HasMaxLength(2100)
                .IsRequired();

            builder.HasOne(x => x.CreatorStore)
                .WithMany(x => x.PhotoFrames);


            builder.HasIndex(x => x.GlobalId);

        }
    }
}
