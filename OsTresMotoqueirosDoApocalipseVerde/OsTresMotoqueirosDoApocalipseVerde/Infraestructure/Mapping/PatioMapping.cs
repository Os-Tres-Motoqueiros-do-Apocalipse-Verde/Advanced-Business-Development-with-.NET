using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entity;

public class PatioMapping : IEntityTypeConfiguration<Patio>
{
    public void Configure(EntityTypeBuilder<Patio> builder)
    {
        builder
            .ToTable("PATIO");

        builder
            .HasKey(p => p.Id);

        builder
            .Property(p => p.Id)
            .HasColumnName("ID_PATIO")
            .HasDefaultValueSql("PATIO_SEQ.NEXTVAL")
            .IsRequired();

        builder
            .Property(p => p.TotalMotos)
            .IsRequired()
            .HasColumnName("TOTAL_MOTOS");

        builder
            .Property(p => p.CapacidadeMoto)
            .IsRequired()
            .HasColumnName("CAPACIDADE_MOTO");

        builder
            .Property(p => p.Localizacao)
            .IsRequired()
            .HasColumnName("LOCALIZACAO");

        builder
            .Property(p => p.FilialId)
            .HasColumnName("ID_FILIAL")
            .IsRequired();

    }
}
