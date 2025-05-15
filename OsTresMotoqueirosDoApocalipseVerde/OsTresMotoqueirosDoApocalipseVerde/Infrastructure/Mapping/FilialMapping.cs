using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entity;

namespace OsTresMotoqueirosDoApocalipseVerde.Infrastructure.Mapping
{
    public class FilialMapping : IEntityTypeConfiguration<Filial>
    {
        public void Configure(EntityTypeBuilder<Filial> builder)
        {
            builder
                .ToTable("filial");

            builder
                .HasKey(f => f.IdFilial);

            builder
                .Property(f => f.IdFilial)
                .IsRequired()
                .HasMaxLength(17);

            builder
                .Property(f => f.NomeFilial)
                .HasMaxLength(150);

            builder
                .Property(f => f.IdResponsavel)
                .IsRequired();

            builder
                .HasOne(f => f.IdResponsavel)
                .WithMany()
                .HasForeignKey(f => f.IdResponsavel)
                .OnDelete(DeleteBehavior.Restrict);


            builder
                .Property(f => f.Endereco)
                .IsRequired();

            builder
                .HasOne(f => f.Endereco)
                .WithMany()
                .HasForeignKey(f => f.Endereco)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property(f => f.Patio)
                .IsRequired();

            builder
                .HasOne(f => f.Patio)
                .WithMany()
                .HasForeignKey(f => f.Patio)
                .OnDelete(DeleteBehavior.Restrict);

            builder
               .HasMany(f => f.Funcionarios)
               .WithOne(f => f.Filial)
               .HasForeignKey(f => f.FilialId)
               .OnDelete(DeleteBehavior.Restrict);

            builder
             .Metadata
             .FindNavigation(nameof(Filial.Funcionarios))!
             .SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
