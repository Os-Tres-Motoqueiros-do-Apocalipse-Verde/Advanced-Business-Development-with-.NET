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
            .Property(s => s.NomeSetor)
            .HasColumnName("NOME_SETOR")
            .IsRequired();

        builder
            .Property(s => s.QtdMoto)
            .HasColumnName("QTD_MOTO")
            .IsRequired();

        builder
            .Property(s => s.Capacidade)
            .HasColumnName("CAPACIDADE")
            .IsRequired();

        builder
            .Property(s => s.Descricao)
            .HasColumnName("DESCRICAO");

        builder
            .Property(s => s.Cor)
            .HasColumnName("COR");

        builder
            .Property(s => s.Localizacao)
            .HasColumnName("LOCALIZACAO");

        builder
            .Property(s => s.PatioId)
            .HasColumnName("ID_PATIO")
            .IsRequired();

        builder
            .HasOne(s => s.Patio)
            .WithMany(p => p.Setores)
            .HasForeignKey(s => s.PatioId)
            .OnDelete(DeleteBehavior.Restrict); 
    }
}
