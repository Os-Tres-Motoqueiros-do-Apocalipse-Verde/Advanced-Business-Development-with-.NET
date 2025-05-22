using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entity;

namespace OsTresMotoqueirosDoApocalipseVerde.Infrastructure.Mapping
{
    public class DadosMap : IEntityTypeConfiguration<Dados>
    {
        public void Configure(EntityTypeBuilder<Dados> builder)
        {
            builder
                .ToTable("DADOS");


            builder
                .HasKey(d => d.Id);

            builder
                .Property(d => d.CPF)
                .IsRequired()
                .HasMaxLength(11);

            builder
                .Property(d => d.Telefone)
                .IsRequired()
                .HasMaxLength(13);

            builder
                .Property(d => d.Email)
                .IsRequired()
                .HasMaxLength(255);

            builder
                .Property(d => d.Senha)
                .IsRequired()
                .HasMaxLength(255);

            builder
                .Property(d => d.Nome)
                .IsRequired()
                .HasMaxLength(150);
        }
    }
}