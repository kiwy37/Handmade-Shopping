using Daw.DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace Daw.DataLayer.DataBaseConenction
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<UserProduct> UserProducts { get; set; }
    }
}
