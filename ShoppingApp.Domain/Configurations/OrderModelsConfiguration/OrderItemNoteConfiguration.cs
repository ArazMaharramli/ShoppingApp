using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingApp.Domain.Models.Domain.OrderModels;

namespace ShoppingApp.Domain.Configurations.OrderModelsConfiguration
{
    public class OrderItemNoteConfiguration : IEntityTypeConfiguration<OrderItemNote>
    {
        public void Configure(EntityTypeBuilder<OrderItemNote> builder)
        {
            builder.Property(x => x.Note)
                .IsRequired();

            builder.HasOne(x => x.OrderItem)
                .WithMany(x => x.OrderItemNotes);


            builder.HasIndex(x => x.UniqueId);

        }
    }
}
