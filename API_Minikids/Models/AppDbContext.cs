using Microsoft.EntityFrameworkCore;

namespace API_Minikids.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Evento> Eventos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>()
                .HasMany(c => c.Eventos)
                .WithOne()
                .HasForeignKey(e => e.ClienteId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
