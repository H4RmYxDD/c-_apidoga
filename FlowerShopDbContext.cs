using Microsoft.EntityFrameworkCore;

namespace apidoga
{
    public class FlowerShopDbContext(DbContextOptions<FlowerShopDbContext> options) : DbContext(options)
    {
        public DbSet<Order> Orders { get; set; }
    }
}
