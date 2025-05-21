using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entity;

namespace OsTresMotoqueirosDoApocalipseVerde.Infrastructure.Mapping
{
    public class SituacaoMapping: IEntityTypeConfiguration<Situacao>
    {
        public void Configure(EntityTypeBuilder<Situacao> builder)
        {
            builder
                .ToTable("situacao");

            builder
                .HasKey(s => s.IdSituacao);

            builder
                .Property(s => s.IdSituacao)
                .IsRequired()
                .HasMaxLength(14);
            
            builder
                .Property(s => s.Nome)
                .IsRequired()
                .HasMaxLength(250);
            
            builder
                .Property(s => s.Descricao)
                .HasMaxLength(250);
            
            builder
                .Property(s => s.Status)
                .IsRequired()
                .HasMaxLength(50);
            
        }
    }
}
