using Microsoft.EntityFrameworkCore;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entities;

namespace OsTresMotoqueirosDoApocalipseVerde.Infraestructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Motorista> Motorista { get; set; }

        public DbSet<Dados> Dados { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.HasSequence<int>("DADOS_SEQ").StartsAt(1).IncrementsBy(1);
            modelBuilder.HasSequence<int>("MOTORISTA_SEQ").StartsAt(1).IncrementsBy(1);


            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        }
    }

}
