using Microsoft.EntityFrameworkCore;
using System.Xml;

namespace GeneradorCompras.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Compra> Compras { get; set; }
        public DbSet<Negocio> Negocios { get; set; }
        public DbSet<Product> Productos { get; set; }
        public DbSet<Tarjeta> Tarjetas { get; set; }
        public DbSet<User> Usuarios { get; set; }

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
