using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entity;

namespace OsTresMotoqueirosDoApocalipseVerde.Infrastructure.Mapping
{
    public class EnderecoMapping : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder
                .ToTable("endereco");

            builder
                .HasKey(e => e.IdEndereco);

            builder
                .Property(e => e.IdEndereco)
                .IsRequired()
                .HasMaxLength(20);

            builder
                .Property(e => e.Numero)
                .IsRequired()
                .HasMaxLength(4);

            builder
                .Property(e => e.Estado)
                .IsRequired()
                .HasMaxLength(30);

            builder
                .Property(e => e.CodigoPais)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(e => e.CodigoPostal)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(e => e.Complemento)
                .HasMaxLength(150);

            builder
                .Property(e => e.Rua)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
