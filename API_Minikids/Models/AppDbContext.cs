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
        public DbSet<Pagamento> Pagamento { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Caixa> Caixa { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Evento>()
       .HasMany(e => e.Pagamentos)
       .WithOne() // Sem propriedade de navegação inversa em Pagamento
       .HasForeignKey(p => p.EventoId);

            modelBuilder.Entity<Pagamento>()
           .HasKey(p => p.Id); // Define o Id como a chave primária
            modelBuilder.Entity<Pagamento>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd(); // Configura o Id como auto incremento

            modelBuilder.Entity<Cliente>()
                .HasMany(c => c.Eventos)
                .WithOne()
                .HasForeignKey(e => e.ClienteId);


            base.OnModelCreating(modelBuilder);
        }
    }
}
