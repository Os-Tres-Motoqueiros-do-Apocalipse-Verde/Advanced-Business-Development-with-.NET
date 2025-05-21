using Microsoft.EntityFrameworkCore;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entity;
using OsTresMotoqueirosDoApocalipseVerde.Infrastructure.Mapping;

namespace OsTresMotoqueirosDoApocalipseVerde.Infrastructure.Context
{
    public class MottuContext(DbContextOptions<MottuContext> options) : DbContext(options)
    {
        public DbSet<Dados> Dados { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Filial> Filiais { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Modelo> Modelos { get; set; }
        public DbSet<Moto> Motos { get; set; }
        public DbSet<Motorista> Motoristas { get; set; }
        public DbSet<Patio> Patios { get; set; }
        public DbSet<Setor> Setores { get; set; }
        public DbSet<Situacao> Situacoes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MottuContext).Assembly);
        }

    }
}
