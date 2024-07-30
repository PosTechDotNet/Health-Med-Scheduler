﻿using HealthMedScheduler.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthMedScheduler.Infrastructure.Data.Configurations
{
    public class EspecialidadeConfiguration : IEntityTypeConfiguration<Especialidade>
    {
        public void Configure(EntityTypeBuilder<Especialidade> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
               .IsRequired()
               .HasMaxLength(150)
                .HasColumnName("nome");

            builder.Property(c => c.Descricao)
               .HasMaxLength(250)
               .HasColumnName("descricao");

            // 1 : N => Especialide : EspecialidadeMedico
            builder.HasMany(c => c.EspecialidadeMedicos)
                .WithOne(c => c.Especialidade)
                .HasForeignKey(c => c.EspecialidadeId);

            builder.ToTable("especialidade");
        }
    }
}
