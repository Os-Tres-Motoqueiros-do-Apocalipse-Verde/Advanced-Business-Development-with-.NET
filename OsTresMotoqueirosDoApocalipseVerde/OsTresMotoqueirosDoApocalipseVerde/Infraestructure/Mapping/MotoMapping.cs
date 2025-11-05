using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entity;

namespace OsTresMotoqueirosDoApocalipseVerde.Infrastructure.Mapping
{
    public class MotoMap : IEntityTypeConfiguration<Moto>
    {
        public void Configure(EntityTypeBuilder<Moto> builder)
        {
            builder
                .ToTable("MOTO");

            builder
                .HasKey(m => m.Id);

            builder
                .Property(m => m.Id)
                .HasDefaultValueSql("MOTO_SEQ.NEXTVAL")
                .HasColumnName("ID_MOTO");


            builder
                .Property(m => m.Placa)
                .HasColumnName("PLACA")
                .HasMaxLength(7);

            builder
                .Property(m => m.Chassi)
                .HasColumnName("CHASSI")
                .HasMaxLength(17);

            builder
                .Property(m => m.Condicao)
                .HasColumnName("CONDICAO")
                .IsRequired()
                .HasMaxLength(8);

            builder
                .Property(m => m.LocalizacaoMoto)
                .HasColumnName("LOCALIZACAO_MOTO")
                .IsRequired();

            builder
                .Property(m => m.MotoristaId)
                .HasColumnName("ID_MOTORISTA")
                .IsRequired();

            builder
                .HasOne(m => m.Motorista)
                .WithOne(m => m.Moto)
                .HasForeignKey<Moto>(m => m.MotoristaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property(m => m.ModeloId)
                .HasColumnName("ID_MODELO")
                .IsRequired();

            builder
                .HasOne(m => m.Modelo)
                .WithOne(m => m.Moto)
                .HasForeignKey<Moto>(m => m.ModeloId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property(m => m.SetorId)
                .HasColumnName("ID_SETOR")
                .IsRequired();

            builder
                .HasOne(m => m.Setor)
                .WithMany(m => m.Motos)
                .HasForeignKey(m => m.SetorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property(m => m.SituacaoId)
                .HasColumnName("ID_SITUACAO")
                .IsRequired();

            builder
                .HasOne(m => m.Situacao)
                .WithOne(s => s.Moto)
                .HasForeignKey<Moto>(m => m.SituacaoId)
                .OnDelete(DeleteBehavior.Restrict);



        }
    }
}
