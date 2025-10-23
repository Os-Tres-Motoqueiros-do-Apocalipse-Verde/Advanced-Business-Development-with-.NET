using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entity;

namespace OsTresMotoqueirosDoApocalipseVerde.Infrastructure.Mapping
{
    public class UsuariosMapping : IEntityTypeConfiguration<Usuarios>
    {
        public void Configure(EntityTypeBuilder<Usuarios> builder)
        {
            builder
                .ToTable("USUARIOS");


            builder
                .HasKey(d => d.Id);

            builder
                .Property(d => d.Id)
                .HasColumnName("ID_USER")
                .HasDefaultValueSql("USUARIOS_SEQ.NEXTVAL")
                .IsRequired();

            builder
                .Property(d => d.Username)
                .IsRequired()
                .HasColumnName("USERNAME")
                .HasMaxLength(150);

            builder
                .Property(d => d.Password)
                .IsRequired()
                .HasColumnName("PASSWORD")
                .HasMaxLength(200);

            builder
                .Property(d => d.Role)
                .IsRequired()
                .HasConversion<string>()
                .HasColumnName("ROLE")
                .HasMaxLength(10);
        }
    }
}