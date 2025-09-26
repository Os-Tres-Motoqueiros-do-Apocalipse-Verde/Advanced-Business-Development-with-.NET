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
                .ToTable("MODELO");


            builder
                .HasKey(d => d.Id);

            builder
                .Property(d => d.Id)
                .HasColumnName("ID_MODELO")
                .HasDefaultValueSql("MODELO_SEQ.NEXTVAL")
                .IsRequired();

            builder
                .Property(d => d.NomeModelo)
                .IsRequired()
                .HasColumnName("NOME_MODELO")
                .HasMaxLength(25);

            builder
                .Property(d => d.Frenagem)
                .HasConversion<string>()
                .HasColumnName("FRENAGEM")
                .HasMaxLength(50);

            builder
                .Property(d => d.SistemaPartida)
                .HasConversion<string>()
                .HasColumnName("SIS_PARTIDA")
                .HasMaxLength(100);

            builder
                .Property(d => d.Tanque)
                .HasColumnName("TANQUE")
                .IsRequired();

            builder
                .Property(d => d.TipoCombustivel)
                .HasConversion<string>()
                .HasColumnName("TIPO_COMBUSTIVEL")
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(d => d.Consumo)
                .HasColumnName("CONSUMO")
                .IsRequired();

            builder
                .HasOne(e => e.Moto)
                .WithOne(f => f.Modelo)
                .HasForeignKey<Moto>(f => f.ModeloId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}