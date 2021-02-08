using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingApp.Domain.Models.Domain.StoreModels;

namespace ShoppingApp.Domain.Configurations.StoreModelsConfiguration
{
    public class StoreContactConfiguration : IEntityTypeConfiguration<StoreContact>
    {
        public void Configure(EntityTypeBuilder<StoreContact> builder)
        {
            builder.Property(x => x.PhoneNumber)
                .IsRequired();

            builder.HasOne(x => x.Store)
                .WithMany(x => x.StoreContacts);



            builder.HasIndex(x => x.GlobalId);

        }
    }


}
