using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entity;

namespace OsTresMotoqueirosDoApocalipseVerde.Infrastructure.Mapping
{
    public class EnderecoMapping : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder
                .ToTable("ENDERECO");


            builder
                .HasKey(e => e.Id);

            builder
                .Property(e => e.Id)
                .HasColumnName("ID_ENDERECO")
                .HasDefaultValueSql("ENDERECO_SEQ.NEXTVAL")
                .IsRequired();

            builder
                .Property(e => e.Numero)
                .IsRequired()
                .HasColumnName("NUMERO")
                .HasMaxLength(4);

            builder
                .Property(e => e.Estado)
                .IsRequired()
                .HasColumnName("ESTADO")
                .HasMaxLength(30);

            builder
                .Property(e => e.CodigoPais)
                .IsRequired()
                .HasColumnName("CODIGO_PAIS")
                .HasMaxLength(50);

            builder
                .Property(e => e.CodigoPostal)
                .IsRequired()
                .HasColumnName("CODIGO_POSTAL")
                .HasMaxLength(50);

            builder
                .Property(e => e.Complemento)
                .HasColumnName("COMPLEMENTO")
                .HasMaxLength(150);

            builder
                .Property(e => e.Rua)
                .IsRequired()
                .HasColumnName("RUA")
                .HasMaxLength(100);

            builder
                .HasOne(e => e.Filial)              
                .WithOne(f => f.Endereco)            
                .HasForeignKey<Filial>(f => f.EnderecoId)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}