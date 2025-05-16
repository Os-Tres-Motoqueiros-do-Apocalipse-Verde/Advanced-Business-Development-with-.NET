using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entity;

namespace OsTresMotoqueirosDoApocalipseVerde.Infrastructure.Mapping
{
    public class SetorMapping : IEntityTypeConfiguration<Setor>
    {
        public void Configure(EntityTypeBuilder<Setor> builder)
        {
            builder
                .ToTable("setor");

            builder
                .HasKey(s => s.IdSetor);

            builder
                .Property(s => s.IdSetor)
                .IsRequired()                      
                .HasMaxLength(17);

            builder
                .Property(s => s.QuantidadeMoto)
                .IsRequired()
                .HasMaxLength(3);
            
            builder
                .Property(s => s.Capacidade)
                .IsRequired()
                .HasMaxLength(3);
            
            builder
                .Property(s => s.AreaSetor)
                .IsRequired()
                .HasMaxLength(5);
            
            builder
                .Property(s => s.NomeSetor)
                .HasMaxLength(250);
            
            builder
                .Property(s => s.Descricao)
                .HasMaxLength(250);
            
            builder
                .Property(s => s.PatioId)
                .IsRequired();

            builder
                .HasOne(s => s.Patio)
                .WithMany()
                .HasForeignKey(s => s.PatioId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
