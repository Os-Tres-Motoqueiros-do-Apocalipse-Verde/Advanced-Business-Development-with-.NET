using Microsoft.EntityFrameworkCore;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entity;
using OsTresMotoqueirosDoApocalipseVerde.Infrastructure.Mapping;

namespace OsTresMotoqueirosDoApocalipseVerde.Infrastructure.Context
{
    public class MottuContext(DbContextOptions<MottuContext> options) : DbContext(options)
    {
        public DbSet<Motorista> Dados { get; set; }
        public DbSet<Motorista> Endereco { get; set; }
        public DbSet<Motorista> Filial { get; set; }
        public DbSet<Motorista> Funcionario { get; set; }
        public DbSet<Motorista> Modelo { get; set; }
        public DbSet<Motorista> Moto { get; set; }
        public DbSet<Motorista> Motoristas { get; set; }
        public DbSet<Motorista> Patio { get; set; }
        public DbSet<Motorista> Setor { get; set; }
        public DbSet<Motorista> Situacao { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DadosMapping());
            modelBuilder.ApplyConfiguration(new EnderecoMapping());
            modelBuilder.ApplyConfiguration(new FilialMapping());
            modelBuilder.ApplyConfiguration(new FuncionarioMapping());
            modelBuilder.ApplyConfiguration(new ModeloMapping());
            modelBuilder.ApplyConfiguration(new MotoMapping());
            modelBuilder.ApplyConfiguration(new MotoristaMapping());
            modelBuilder.ApplyConfiguration(new PatioMapping());
            modelBuilder.ApplyConfiguration(new SetorMapping());
            modelBuilder.ApplyConfiguration(new SituacaoMapping());

        }
    }
}
