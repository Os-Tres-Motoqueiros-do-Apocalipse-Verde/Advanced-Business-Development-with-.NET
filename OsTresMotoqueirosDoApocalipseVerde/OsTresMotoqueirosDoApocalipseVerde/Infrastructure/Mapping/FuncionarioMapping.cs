using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entity;

namespace OsTresMotoqueirosDoApocalipseVerde.Infrastructure.Mapping
{
    public class FuncionarioMapping : IEntityTypeConfiguration<Funcionario>
    {
        public void Configure(EntityTypeBuilder<Funcionario> builder)
        {
            builder
                .ToTable("funcionario");

            builder
                .HasKey(f => f.IdFuncionario);

            builder
                .Property(f => f.IdFuncionario)
                .IsRequired();

            builder
                .Property(f => f.Cargo)
                .IsRequired()                      
                .HasMaxLength(100);                

            builder
                .Property(f => f.DadosId)
                .IsRequired();

            builder
                .HasOne(f => f.Dados)
                .WithMany()
                .HasForeignKey(f => f.DadosId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
