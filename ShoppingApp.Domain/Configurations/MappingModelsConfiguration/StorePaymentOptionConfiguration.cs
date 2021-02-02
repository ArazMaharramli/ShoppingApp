using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingApp.Domain.Models.Domain.MappingModels;

namespace ShoppingApp.Domain.Configurations.MappingModelsConfiguration
{
    public class StorePaymentOptionConfiguration : IEntityTypeConfiguration<StorePaymentOption>
    {
        public void Configure(EntityTypeBuilder<StorePaymentOption> builder)
        {
            builder.HasOne(x => x.Store)
             .WithMany(x => x.StorePaymentOptions);

            builder.HasOne(x => x.PaymentOption)
                .WithMany(x => x.StorePaymentOptions);

            builder.HasKey(x => new { x.PaymentOptionId, x.StoreId });
        }
    }
}
