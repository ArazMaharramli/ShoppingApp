using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingApp.Domain.Models.Domain.StoreModels;

namespace ShoppingApp.Domain.Configurations.StoreModelsConfiguration
{
    public class StoreTypeConfiguration : IEntityTypeConfiguration<StoreType>
    {
        public void Configure(EntityTypeBuilder<StoreType> builder)
        {
            builder.Property(x => x.Name)
                .IsRequired();


            builder.HasIndex(x => x.UniqueId);

        }
    }


}
