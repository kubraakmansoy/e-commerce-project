using App.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Data.Contexts
{
    public class AppDbContext : DbContext
    {

        public DbSet<User> Users { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Role> Roles { get; set; }

        public DbSet<Order> Orders { get; set; }



        // İleride buraya diğer entity'ler de eklenecek: Users, Categories, Orders vs.
    }
}
