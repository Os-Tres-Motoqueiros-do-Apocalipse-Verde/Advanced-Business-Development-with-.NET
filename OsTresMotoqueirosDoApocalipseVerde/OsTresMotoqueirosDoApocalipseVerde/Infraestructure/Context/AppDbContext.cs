using Microsoft.EntityFrameworkCore;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entity;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace OsTresMotoqueirosDoApocalipseVerde.Infraestructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Dados> Dados { get; set; }
        public DbSet<Moto> Moto { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasSequence<long>("USUARIO_SEQ").StartsAt(10).IncrementsBy(1);
            modelBuilder.HasSequence<long>("DESASTRE_SEQ").StartsAt(10).IncrementsBy(1);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
