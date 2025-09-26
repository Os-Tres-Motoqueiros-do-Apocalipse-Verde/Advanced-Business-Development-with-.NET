using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entity;

namespace OsTresMotoqueirosDoApocalipseVerde.Infrastructure.Mapping
{
    public class SituacaoMapping : IEntityTypeConfiguration<Situacao>
    {
        public void Configure(EntityTypeBuilder<Situacao> builder)
        {
            builder
                .ToTable("SITUACAO");


            builder
                .HasKey(s => s.Id);

            builder
                .Property(s => s.Id)
                .HasColumnName("ID_SITUACAO")
                .HasDefaultValueSql("SITUACAO_SEQ.NEXTVAL")
                .IsRequired();

            builder
                .Property(s => s.Nome)
                .IsRequired()
                .HasColumnName("NOME")
                .HasMaxLength(250);

            builder
                .Property(s => s.Descricao)
                .HasColumnName("DESCRICAO")
                .HasMaxLength(250);

            builder
                .Property(s => s.Status)
                .IsRequired()
                .HasConversion<string>()
                .HasColumnName("STATUS");

            builder
                .HasOne(s => s.Moto)
                .WithOne(m => m.Situacao)
                .HasForeignKey<Moto>(m => m.SituacaoId)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}