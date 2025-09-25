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
        public DbSet<Setor> Setor { get; set; }
        public DbSet<Situacao> Situacao { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasSequence<long>("DADOS_SEQ").StartsAt(1000).IncrementsBy(1);
            modelBuilder.HasSequence<long>("MOTORISTA_SEQ").StartsAt(1000).IncrementsBy(1);
            modelBuilder.HasSequence<long>("MODELO_SEQ").StartsAt(1000).IncrementsBy(1);
            modelBuilder.HasSequence<long>("MOTO_SEQ").StartsAt(1000).IncrementsBy(1);
            modelBuilder.HasSequence<long>("ENDERECO_SEQ").StartsAt(1000).IncrementsBy(1);
            modelBuilder.HasSequence<long>("FILIAL_SEQ").StartsAt(1000).IncrementsBy(1);
            modelBuilder.HasSequence<long>("FUNCIONARIO_SEQ").StartsAt(1000).IncrementsBy(1);
            modelBuilder.HasSequence<long>("PATIO_SEQ").StartsAt(1000).IncrementsBy(1);
            modelBuilder.HasSequence<long>("SETOR_SEQ").StartsAt(1000).IncrementsBy(1);
            modelBuilder.HasSequence<long>("SITUACAO_SEQ").StartsAt(1000).IncrementsBy(1);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }

        

    }
}