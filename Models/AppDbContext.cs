using Microsoft.EntityFrameworkCore;
using System.Xml;

namespace GeneradorCompras.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<MyEntity> MyEntities { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
