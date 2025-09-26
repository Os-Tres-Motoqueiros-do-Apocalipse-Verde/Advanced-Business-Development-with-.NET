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
                .ToTable("FILIAL");


            builder
                .HasKey(f => f.Id);

            builder
                .Property(f => f.Id)
                .HasColumnName("ID_FILIAL")
                .HasDefaultValueSql("FILIAL_SEQ.NEXTVAL")
                .IsRequired();

            builder
                .Property(f => f.NomeFilial)
                .HasColumnName("NOME_FILIAL")
                .HasMaxLength(150);

            builder
                .Property(f => f.EnderecoId)
                .HasColumnName("ID_ENDERECO")
                .IsRequired();

            builder
                .HasOne(f => f.Endereco)
                .WithOne(e => e.Filial)
                .HasForeignKey<Filial>(f => f.EnderecoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(f => f.Patios)
                .WithOne(p => p.Filial)
                .HasForeignKey(p => p.FilialId)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}