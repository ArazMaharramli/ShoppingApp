using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ShoppingApp.Domain.Data;

namespace ShoppingApp.Domain
{
    public class ShoppingAppDbContextFactory : IDesignTimeDbContextFactory<ShoppingAppDbContext>
    {
        public ShoppingAppDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ShoppingAppDbContext>();
            builder.UseSqlServer(
                "Data Source=localhost;Initial Catalog=ShoppingAppDB;User Id=sa;Password=<YourStrong@Passw0rd>;"
                );
            return new ShoppingAppDbContext(builder.Options);
        }
    }
}
