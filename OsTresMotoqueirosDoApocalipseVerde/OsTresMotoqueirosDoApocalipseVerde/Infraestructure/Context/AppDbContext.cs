using Microsoft.EntityFrameworkCore;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entities;
using System.Data;

namespace OsTresMotoqueirosDoApocalipseVerde.Infraestructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Motorista> Motorista { get; set; }
        public DbSet<Dados> Dados { get; set; }
        public DbSet<Moto> Moto { get; set; }
        public DbSet<Modelo> Modelo { get; set; }
        public DbSet<Endereco> Endereco { get; set; }
        public DbSet<Filial> Filial { get; set; }
        public DbSet<Funcionario> Funcionario { get; set; }
        public DbSet<Patio> Patio { get; set; }
        public DbSet<Regiao> Regiao { get; set; }
        public DbSet<Setor> Setor { get; set; }
        public DbSet<Situacao> Situacao { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasSequence<int>("DADOS_SEQ").StartsAt(1000).IncrementsBy(1);
            modelBuilder.HasSequence<int>("MOTORISTA_SEQ").StartsAt(1000).IncrementsBy(1);
            modelBuilder.HasSequence<int>("MODELO_SEQ").StartsAt(1000).IncrementsBy(1);
            modelBuilder.HasSequence<int>("MOTO_SEQ").StartsAt(1000).IncrementsBy(1);
            modelBuilder.HasSequence<int>("ENDERECO_SEQ").StartsAt(1000).IncrementsBy(1);
            modelBuilder.HasSequence<int>("FILIAL_SEQ").StartsAt(1000).IncrementsBy(1);
            modelBuilder.HasSequence<int>("FUNCIONARIO_SEQ").StartsAt(1000).IncrementsBy(1);
            modelBuilder.HasSequence<int>("PATIO_SEQ").StartsAt(1000).IncrementsBy(1);
            modelBuilder.HasSequence<int>("REGIAO_SEQ").StartsAt(1000).IncrementsBy(1);
            modelBuilder.HasSequence<int>("SETOR_SEQ").StartsAt(1000).IncrementsBy(1);
            modelBuilder.HasSequence<int>("SITUACAO_SEQ").StartsAt(1000).IncrementsBy(1);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }

        

    }
}