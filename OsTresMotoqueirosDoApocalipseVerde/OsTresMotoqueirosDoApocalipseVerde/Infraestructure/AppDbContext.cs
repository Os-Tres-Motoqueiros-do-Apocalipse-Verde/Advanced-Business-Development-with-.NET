using Microsoft.EntityFrameworkCore;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entities;
using System.Data;

namespace OsTresMotoqueirosDoApocalipseVerde.Infraestructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Motorista> Motorista { get; set; }
        public DbSet<Dados> Dados { get; set; }
        public DbSet<Moto> Moto { get; set; }
        public DbSet<Modelo> Modelo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasSequence<int>("DADOS_SEQ").StartsAt(1000).IncrementsBy(1);
            modelBuilder.HasSequence<int>("MOTORISTA_SEQ").StartsAt(1000).IncrementsBy(1);
            modelBuilder.HasSequence<int>("MODELO_SEQ").StartsAt(1000).IncrementsBy(1);
            modelBuilder.HasSequence<int>("MOTO_SEQ").StartsAt(1000).IncrementsBy(1);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }

        

    }
}