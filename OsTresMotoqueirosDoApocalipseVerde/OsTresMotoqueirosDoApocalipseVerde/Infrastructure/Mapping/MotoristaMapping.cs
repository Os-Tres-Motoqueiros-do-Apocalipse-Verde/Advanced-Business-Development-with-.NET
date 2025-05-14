using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entity;

namespace OsTresMotoqueirosDoApocalipseVerde.Infrastructure.Mapping
{
    public class MotoristaMapping : IEntityTypeConfiguration<Motorista>
    {
        public void Configure(EntityTypeBuilder<Motorista> builder)
        {
            builder
                .ToTable("motorista");

            builder
                .HasKey(m => m.IdMotorista);

            builder
                .Property(m => m.Plano)
                .IsRequired()                      // NOT NULL no banco
                .HasMaxLength(20);                // Limite de caracteres (ABC1234 / ABC1D23)

            builder
                .Property(m => m.DadosId)
                .IsRequired();

            builder
                .HasOne(m => m.DadosCpf)
                .WithMany()
                .HasForeignKey(m => m.DadosId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
