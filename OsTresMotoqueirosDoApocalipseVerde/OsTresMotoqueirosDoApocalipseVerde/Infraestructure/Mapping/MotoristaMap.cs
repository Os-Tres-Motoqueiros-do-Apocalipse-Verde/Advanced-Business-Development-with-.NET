using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entity;

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
                .Property(m => m.Id)
                .HasDefaultValueSql("MOTORISTA_SEQ.NEXTVAL")
                .HasColumnName("ID_MOTORISTA");


            builder
                .Property(m => m.Plano)
                .HasConversion<string>()
                .HasColumnName("PLANO")
                .IsRequired()
                .HasMaxLength(20);

            builder
                .Property(m => m.DadosId)
                .HasColumnName("ID_DADOS")
                .IsRequired();

            builder
                .HasOne(m => m.Dados)
                .WithOne(m => m.Motorista)
                .HasForeignKey<Motorista>(m => m.DadosId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
