using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entities;

namespace OsTresMotoqueirosDoApocalipseVerde.Infrastructure.Mapping
{
    public class MotoristaMap : IEntityTypeConfiguration<Motorista>
    {
        public void Configure(EntityTypeBuilder<Motorista> builder)
        {
            builder
                .ToTable("MOTORISTA");

            builder
                .HasKey(m => m.Id);

            builder
                .Property(m => m.Id);


            builder
                .Property(m => m.Plano)
                .HasConversion<string>()
                .IsRequired()
                .HasMaxLength(20);

            builder
                .Property(m => m.DadosId)
                .IsRequired();

            builder
                .HasOne(m => m.Dados)
                .WithMany()
                .HasForeignKey(m => m.DadosId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
