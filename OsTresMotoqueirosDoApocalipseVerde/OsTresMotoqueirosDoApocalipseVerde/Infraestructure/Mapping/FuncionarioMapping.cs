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
                .ToTable("FUNCIONARIO");


            builder
                .HasKey(f => f.Id);

            builder
                .Property(f => f.Id)
                .HasColumnName("ID_FUNC")
                .HasDefaultValueSql("FUNCIONARIO_SEQ.NEXTVAL")
                .IsRequired();

            builder
                .Property(f => f.Cargo)
                .IsRequired()
                .HasConversion<string>()
                .HasColumnName("CARGO")
                .HasMaxLength(100);

            builder
                .Property(f => f.DadosId)
                .HasColumnName("ID_DADOS");

            builder
                .HasOne(f => f.Dados)
                .WithOne(f => f.Funcionario)
                .HasForeignKey<Funcionario>(f => f.DadosId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property(f => f.FilialId)
                .HasColumnName("ID_FILIAL");

            builder
                .HasOne(f => f.Filial)
                .WithMany(filial => filial.Funcionarios)
                .HasForeignKey(f => f.FilialId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}