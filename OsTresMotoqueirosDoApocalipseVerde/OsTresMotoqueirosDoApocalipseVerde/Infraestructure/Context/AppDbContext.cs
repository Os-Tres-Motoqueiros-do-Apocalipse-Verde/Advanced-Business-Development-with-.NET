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
        public DbSet<Endereco> Endereco { get; set; }
        public DbSet<Funcionario> Funcionario { get; set; }
        public DbSet<Filial> Filial { get; set; }
        public DbSet<Modelo> Modelo { get; set; }
        public DbSet<Motorista> Motorista { get; set; }
        public DbSet<Patio> Patio { get; set; }
        public DbSet<Setor> Setor { get; set; }
        public DbSet<Situacao> Situacao { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
