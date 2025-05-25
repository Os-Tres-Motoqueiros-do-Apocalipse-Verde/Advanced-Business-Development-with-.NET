using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entities;

namespace OsTresMotoqueirosDoApocalipseVerde.Infrastructure.Mapping
{
    public class MotoMap : IEntityTypeConfiguration<Moto>
    {
        public void Configure(EntityTypeBuilder<Moto> builder)
        {
            builder
                .ToTable("MOTO");

            builder
                .HasKey(m => m.Id);

            builder
                .Property(m => m.Id)
                .HasDefaultValueSql("MOTO_SEQ.NEXTVAL")
                .HasColumnName("ID_MOTO");


            builder
                .Property(m => m.Placa)
                .HasColumnName("PLACA")
                .HasMaxLength(7);

            builder
                .Property(m => m.Chassi)
                .HasColumnName("CHASSI")
                .HasMaxLength(17);

            builder
                .Property(m => m.Condicao)
                .HasColumnName("CONDICAO")
                .IsRequired()
                .HasMaxLength(8);

            builder
                .Property(m => m.Latitude)
                .HasColumnName("LATITUDE")
                .IsRequired()
                .HasMaxLength(5);

            builder
                .Property(m => m.Longitude)
                .HasColumnName("LONGITUDE")
                .IsRequired()
                .HasMaxLength(5);

            builder
                .Property(m => m.ModeloId)
                .HasColumnName("ID_MODELO")
                .IsRequired();

            builder
                .HasOne(m => m.Modelo)
                .WithMany()
                .HasForeignKey(m => m.ModeloId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
