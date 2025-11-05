using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entity;

namespace OsTresMotoqueirosDoApocalipseVerde.Infrastructure.Mapping
{
    public class DadosMapping : IEntityTypeConfiguration<Dados>
    {
        public void Configure(EntityTypeBuilder<Dados> builder)
        {
            builder
                .ToTable("DADOS");


            builder
                .HasKey(d => d.Id);

            builder
                .Property(d => d.Id)
                .HasColumnName("ID_DADOS")
                .HasDefaultValueSql("DADOS_SEQ.NEXTVAL")
                .IsRequired();

            builder
                .Property(d => d.Nome)
                .HasColumnName("NOME")
                .IsRequired()
                .HasMaxLength(150);

            builder
                .Property(d => d.CPF)
                .IsRequired()
                .HasColumnName("CPF")
                .HasMaxLength(11);

            builder
                .Property(d => d.Telefone)
                .HasColumnName("TELEFONE")
                .HasMaxLength(13);

            builder
                .Property(d => d.Email)
                .HasColumnName("EMAIL")
                .HasMaxLength(255);

            builder
                .Property(d => d.Senha)
                .HasColumnName("SENHA")
                .HasMaxLength(255);


            builder
                .HasOne(e => e.Funcionario)
                .WithOne(f => f.Dados)
                .HasForeignKey<Funcionario>(f => f.DadosId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(e => e.Motorista)
                .WithOne(f => f.Dados)
                .HasForeignKey<Motorista>(f => f.DadosId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}