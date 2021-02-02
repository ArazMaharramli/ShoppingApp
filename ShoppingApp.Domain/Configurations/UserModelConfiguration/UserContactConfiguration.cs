using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingApp.Domain.Models.Domain.UserModels;

namespace ShoppingApp.Domain.Configurations.UserModelConfiguration
{
    public class UserContactConfiguration : IEntityTypeConfiguration<UserContact>
    {
        public void Configure(EntityTypeBuilder<UserContact> builder)
        {
            builder.Property(x => x.Value)
                .IsRequired()
                .HasMaxLength(258);

            builder.Property(x => x.ContactType)
                .IsRequired();

            builder.HasOne(x => x.User)
                .WithMany(x => x.UserContacts);


            builder.HasIndex(x => x.UniqueId);

        }
    }
}
