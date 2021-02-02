using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingApp.Domain.Models.Domain.UserModels;

namespace ShoppingApp.Domain.Configurations.UserModelConfiguration
{
    public class UserNotificationTokenConfiguration : IEntityTypeConfiguration<UserNotificationToken>
    {
        public void Configure(EntityTypeBuilder<UserNotificationToken> builder)
        {
            builder.Property(x => x.NotificationToken)
                .IsRequired();

            builder.HasOne(x => x.User)
                .WithMany(x => x.NotificationTokens);


            builder.HasIndex(x => x.UniqueId);

        }
    }
}
