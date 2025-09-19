using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entities;

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
                .HasColumnName("FILIAL_ID");

            builder
                .HasOne(f => f.Filial)
                .WithMany()
                .HasForeignKey(f => f.FilialId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}