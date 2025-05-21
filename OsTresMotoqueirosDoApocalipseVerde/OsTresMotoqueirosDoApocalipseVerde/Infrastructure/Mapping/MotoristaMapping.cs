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
                .ToTable("MOTORISTA");

            builder
                .HasKey(m => m.IdMotorista);

            builder
                .Property(m => m.IdMotorista)
                .HasColumnName("ID_MOTORISTA");


            builder
                .Property(m => m.Plano)
                .HasColumnName("PLANO")
                .HasConversion<string>()
                .IsRequired()
                .HasMaxLength(20);

            builder
                .Property(m => m.DadosId)
                .HasColumnName("ID_DADOS")
                .IsRequired();

            builder
                .HasOne(m => m.Dados)
                .WithMany()
                .HasForeignKey(m => m.DadosId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}