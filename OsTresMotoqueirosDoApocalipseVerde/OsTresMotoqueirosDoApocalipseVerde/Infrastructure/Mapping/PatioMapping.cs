using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entity;

namespace OsTresMotoqueirosDoApocalipseVerde.Infrastructure.Mapping
{
    public class PatioMapping : IEntityTypeConfiguration<Patio>
    {
        public void Configure(EntityTypeBuilder<Patio> builder)
        {
            builder
                .ToTable("patio");

            builder
                .HasKey(p => p.IdPatio);

            builder
                .Property(p => p.IdPatio)
                .ValueGeneratedOnAdd()
                .IsRequired()                      
                .HasMaxLength(30);

            builder
                .HasMany(p => p.Setores)
                .WithOne(p => p.Patio)
                .HasForeignKey(p => p.PatioId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
             .Metadata
             .FindNavigation(nameof(Patio.Setores))!
             .SetPropertyAccessMode(PropertyAccessMode.Field);


            builder
                .HasOne(p => p.Filial)
                .WithMany()
                .HasForeignKey(p => p.FilialId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
