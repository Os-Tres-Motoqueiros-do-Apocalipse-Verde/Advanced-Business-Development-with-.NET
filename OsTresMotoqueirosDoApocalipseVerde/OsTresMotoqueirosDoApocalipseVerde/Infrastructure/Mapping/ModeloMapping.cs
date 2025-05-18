using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entity;

namespace OsTresMotoqueirosDoApocalipseVerde.Infrastructure.Mapping
{
    public class ModeloMapping : IEntityTypeConfiguration<Modelo>
    {
        public void Configure(EntityTypeBuilder<Modelo> builder)
        {
            builder
                .ToTable("modelo");

            builder
                .HasKey(m => m.IdModelo);

            builder
                .Property(m => m.IdModelo)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder
                .Property(m => m.NomeModelo)
                .IsRequired()
                .HasMaxLength(15);

            builder
                .Property(m => m.Frenagem)
                .HasMaxLength(50);

            builder
                .Property(m => m.SistemaPartida)
                .HasMaxLength(100);

            builder
                .Property(m => m.Tanque)
                .IsRequired()
                .HasMaxLength(3);

            builder
                .Property(m => m.TipoCombustivel)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(m => m.Consumo)
                .IsRequired()
                .HasMaxLength(4);
        }
    }
}
