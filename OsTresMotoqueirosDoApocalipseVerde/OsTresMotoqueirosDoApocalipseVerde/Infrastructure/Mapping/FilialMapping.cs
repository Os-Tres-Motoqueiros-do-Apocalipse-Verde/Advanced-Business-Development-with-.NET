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
                .Property(f => f.ResponsavelId)
                .IsRequired()
                .HasMaxLength(7);


            builder
                .HasOne(f => f.Endereco)
                .WithMany()
                .HasForeignKey(f => f.EnderecoId)
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
