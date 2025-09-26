using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entity;

public class SetorMapping : IEntityTypeConfiguration<Setor>
{
    public void Configure(EntityTypeBuilder<Setor> builder)
    {
        builder
            .ToTable("SETOR");

        builder
            .HasKey(s => s.Id);

        builder
            .Property(s => s.Id)
            .HasColumnName("ID_SETOR")
            .HasDefaultValueSql("SETOR_SEQ.NEXTVAL")
            .IsRequired();

        builder
            .Property(s => s.QtdMoto)
            .IsRequired()
            .HasColumnName("QTD_MOTO");

        builder
            .Property(s => s.Capacidade)
            .IsRequired()
            .HasColumnName("CAPACIDADE_MOTO");

        builder
            .Property(s => s.NomeSetor)
            .HasMaxLength(250)
            .HasColumnName("NOME_SETOR");

        builder
            .Property(s => s.Descricao)
            .HasMaxLength(250)
            .HasColumnName("DESCRICAO");

        builder
            .Property(s => s.Localizacao)
            .HasMaxLength(255)
            .IsRequired()
            .HasColumnName("LOCALIZACAO");

        builder
                .Property(s => s.PatioId)
                .HasColumnName("PATIO_ID");

        builder
            .HasOne(s => s.Patio)
            .WithMany()
            .HasForeignKey(s => s.PatioId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
