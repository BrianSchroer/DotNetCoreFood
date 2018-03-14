using DotNetCoreFood.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetCoreFood.Data
{
    public class DotNetCoreFoodDbContext : DbContext
    {
        public DbSet<Restaurant> Restaurants { get; set; }

        public DotNetCoreFoodDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
