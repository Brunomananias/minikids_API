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
            base.OnModelCreating(modelBuilder);

            // Configurar a relação entre Cliente e Evento
            modelBuilder.Entity<Evento>()
                .HasOne(e => e.Cliente)
                .WithMany(c => c.Eventos)
                .HasForeignKey(e => e.ClienteId);
        }
    }
}
