using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingApp.Domain.Models.Domain.UserModels;

namespace ShoppingApp.Domain.Configurations.UserModelConfiguration
{
    public class UserTypeConfiguration : IEntityTypeConfiguration<UserType>
    {
        public void Configure(EntityTypeBuilder<UserType> builder)
        {
            builder.Property(x => x.UniqueName)
                .IsRequired();


            builder.HasIndex(x => x.UniqueId);

        }
    }
}
