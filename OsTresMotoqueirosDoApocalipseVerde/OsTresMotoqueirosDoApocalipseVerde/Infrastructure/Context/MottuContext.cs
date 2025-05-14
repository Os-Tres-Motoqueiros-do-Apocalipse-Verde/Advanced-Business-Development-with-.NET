using Microsoft.EntityFrameworkCore;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entity;
using OsTresMotoqueirosDoApocalipseVerde.Infrastructure.Mapping;

namespace OsTresMotoqueirosDoApocalipseVerde.Infrastructure.Context
{
    public class MottuContext(DbContextOptions<MottuContext> options) : DbContext(options)
    {
        public DbSet<Motorista> Motoristas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MotoristaMapping());

        }
    }
}
