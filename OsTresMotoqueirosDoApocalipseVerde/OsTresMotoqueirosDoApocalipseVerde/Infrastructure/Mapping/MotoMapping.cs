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
                .Property(m => m.intitude)
                .IsRequired()
                .HasMaxLength(5);


            builder
                .HasOne(m => m.Modelo)
                .WithMany()
                .HasForeignKey(m => m.ModeloId)
                .OnDelete(DeleteBehavior.Restrict);


            builder
                .HasOne(m => m.Setor)
                .WithMany()
                .HasForeignKey(m => m.SetorId)
                .OnDelete(DeleteBehavior.Restrict);


            builder
                .HasOne(m => m.Motorista)
                .WithMany()
                .HasForeignKey(m => m.MotoristaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
