using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entity;

namespace OsTresMotoqueirosDoApocalipseVerde.Infrastructure.Mapping
{
    public class MotoMapping : IEntityTypeConfiguration<Moto>
    {
        public void Configure(EntityTypeBuilder<Moto> builder)
        {
            builder
                .ToTable("moto");

            builder
                .HasKey(m => m.IdMoto);

            builder
                .Property(m => m.IdMoto)
                .IsRequired()
                .HasMaxLength(16);

            builder
                .Property(m => m.Placa)
                .HasMaxLength(7);

            builder
                .Property(m => m.Chassi)
                .HasMaxLength(17);

            builder
                .Property(m => m.Condicao)
                .IsRequired()
                .HasMaxLength(8);

            builder
                .Property(m => m.Latitude)
                .IsRequired()
                .HasMaxLength(5);

            builder
                .Property(m => m.Longitude)
                .IsRequired()
                .HasMaxLength(5);


            builder
                .Property(m => m.Modelo)
                .IsRequired();

            builder
                .HasOne(m => m.Modelo)
                .WithMany()
                .HasForeignKey(m => m.Modelo)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property(m => m.Setor)
                .IsRequired();

            builder
                .HasOne(m => m.Setor)
                .WithMany()
                .HasForeignKey(m => m.Setor)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property(m => m.Motorista)
                .IsRequired();

            builder
                .HasOne(m => m.Motorista)
                .WithMany()
                .HasForeignKey(m => m.Motorista)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
