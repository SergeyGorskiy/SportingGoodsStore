using Microsoft.EntityFrameworkCore;
using SportingGoodsStore.Models;

namespace SportingGoodsStore.EF
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }

    }
}