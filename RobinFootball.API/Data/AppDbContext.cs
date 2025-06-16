using Microsoft.EntityFrameworkCore;
using RobinFootball.API.Models;

namespace RobinFootball.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<Player> Players => Set<Player>();
    }
}
