﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entities;

namespace OsTresMotoqueirosDoApocalipseVerde.Infrastructure.Mapping
{
    public class DadosMap : IEntityTypeConfiguration<Dados>
    {
        public void Configure(EntityTypeBuilder<Dados> builder)
        {
            builder
                .ToTable("DADOS");


            builder
                .HasKey(d => d.Id);

            builder
                .Property(d => d.Id)
                .HasColumnName("ID_DADOS")
                .HasDefaultValueSql("DADOS_SEQ.NEXTVAL")
                .IsRequired();

            builder
                .Property(d => d.CPF)
                .IsRequired()
                .HasColumnName("CPF")
                .HasMaxLength(11);

            builder
                .Property(d => d.Telefone)
                .HasColumnName("TELEFONE")
                .IsRequired()
                .HasMaxLength(13);

            builder
                .Property(d => d.Email)
                .HasColumnName("EMAIL")
                .IsRequired()
                .HasMaxLength(255);

            builder
                .Property(d => d.Senha)
                .HasColumnName("SENHA")
                .IsRequired()
                .HasMaxLength(255);

            builder
                .Property(d => d.Nome)
                .HasColumnName("NOME")
                .IsRequired()
                .HasMaxLength(150);
        }
    }
}