using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entities;

namespace OsTresMotoqueirosDoApocalipseVerde.Infrastructure.Mapping
{
    public class RegiaoMapping : IEntityTypeConfiguration<Regiao>
    {
        public void Configure(EntityTypeBuilder<Regiao> builder)
        {
            builder
                .ToTable("REGIAO");


            builder
                .HasKey(r => r.Id);

            builder
                .Property(r => r.Id)
                .HasColumnName("ID_REGIAO")
                .HasDefaultValueSql("REGIAO_SEQ.NEXTVAL")
                .IsRequired();

            builder
                .Property(r => r.Localizacao)
                .IsRequired()
                .HasColumnName("LOCALIZACAO");

            builder
                .Property(r => r.Area)
                .IsRequired()
                .HasColumnName("AREA");

            builder
                .HasOne(r => r.Patio)
                .WithOne(p => p.Regiao)
                .HasForeignKey<Patio>(p => p.RegiaoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(r => r.Setor)
                .WithOne(s => s.Regiao)
                .HasForeignKey<Setor>(s => s.RegiaoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}