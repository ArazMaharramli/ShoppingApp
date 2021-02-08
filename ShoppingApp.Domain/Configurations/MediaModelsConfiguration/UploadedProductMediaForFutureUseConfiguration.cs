using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingApp.Domain.Models.Domain.MediaModels;

namespace ShoppingApp.Domain.Configurations.MediaModelsConfiguration
{
    public class UploadedProductMediaForFutureUseConfiguration : IEntityTypeConfiguration<UploadedProductMediaForFutureUse>
    {
        public void Configure(EntityTypeBuilder<UploadedProductMediaForFutureUse> builder)
        {
            builder.Property(x => x.MediaUrl)
                .IsRequired();

            builder.Property(x => x.ExpiryDate)
                .IsRequired();

            builder.HasOne(x => x.Store)
                .WithMany(x => x.UploadedProductMediasForFutureUse);


            builder.HasIndex(x => x.GlobalId);

        }
    }
}
