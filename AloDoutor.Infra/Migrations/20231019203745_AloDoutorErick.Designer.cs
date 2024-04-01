﻿// <auto-generated />
using System;
using AloDoutor.Infra.Data.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AloDoutor.Infra.Migrations
{
    [DbContext(typeof(MeuDbContext))]
    [Migration("20231019203745_AloDoutorErick")]
    partial class AloDoutorErick
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AloDoutor.Domain.Entity.Agendamento", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataHoraAtendimento")
                        .HasColumnType("datetime");

                    b.Property<Guid>("EspecialidadeMedicoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PacienteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("StatusAgendamento")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EspecialidadeMedicoId");

                    b.HasIndex("PacienteId");

                    b.ToTable("agendamento", (string)null);
                });

            modelBuilder.Entity("AloDoutor.Domain.Entity.Especialidade", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Descricao")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)")
                        .HasColumnName("descricao");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("nome");

                    b.HasKey("Id");

                    b.ToTable("especialidade", (string)null);
                });

            modelBuilder.Entity("AloDoutor.Domain.Entity.EspecialidadeMedico", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataRegistro")
                        .HasColumnType("datetime")
                        .HasColumnName("dataRegistro");

                    b.Property<Guid>("EspecialidadeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MedicoId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("EspecialidadeId");

                    b.HasIndex("MedicoId");

                    b.ToTable("especialidadeMedico", (string)null);
                });

            modelBuilder.Entity("AloDoutor.Domain.Entity.Medico", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Cep")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("varchar")
                        .HasColumnName("cep");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar")
                        .HasColumnName("cpf");

                    b.Property<string>("Crm")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("varchar")
                        .HasColumnName("crm");

                    b.Property<string>("Endereco")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("varchar")
                        .HasColumnName("endereco");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar")
                        .HasColumnName("estado");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar")
                        .HasColumnName("nome");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("varchar")
                        .HasColumnName("telefone");

                    b.HasKey("Id");

                    b.ToTable("medico", (string)null);
                });

            modelBuilder.Entity("AloDoutor.Domain.Entity.Paciente", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Cep")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("varchar")
                        .HasColumnName("cep");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar")
                        .HasColumnName("cpf");

                    b.Property<string>("Endereco")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("varchar")
                        .HasColumnName("endereco");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar")
                        .HasColumnName("estado");

                    b.Property<string>("Idade")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("varchar")
                        .HasColumnName("idade");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar")
                        .HasColumnName("nome");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("varchar")
                        .HasColumnName("telefone");

                    b.HasKey("Id");

                    b.ToTable("paciente", (string)null);
                });

            modelBuilder.Entity("AloDoutor.Domain.Entity.Agendamento", b =>
                {
                    b.HasOne("AloDoutor.Domain.Entity.EspecialidadeMedico", "EspecialidadeMedico")
                        .WithMany("Agendamentos")
                        .HasForeignKey("EspecialidadeMedicoId")
                        .IsRequired();

                    b.HasOne("AloDoutor.Domain.Entity.Paciente", "Paciente")
                        .WithMany("Agendamentos")
                        .HasForeignKey("PacienteId")
                        .IsRequired();

                    b.Navigation("EspecialidadeMedico");

                    b.Navigation("Paciente");
                });

            modelBuilder.Entity("AloDoutor.Domain.Entity.EspecialidadeMedico", b =>
                {
                    b.HasOne("AloDoutor.Domain.Entity.Especialidade", "Especialidade")
                        .WithMany("EspecialidadeMedicos")
                        .HasForeignKey("EspecialidadeId")
                        .IsRequired();

                    b.HasOne("AloDoutor.Domain.Entity.Medico", "Medico")
                        .WithMany("EspecialidadesMedicos")
                        .HasForeignKey("MedicoId")
                        .IsRequired();

                    b.Navigation("Especialidade");

                    b.Navigation("Medico");
                });

            modelBuilder.Entity("AloDoutor.Domain.Entity.Especialidade", b =>
                {
                    b.Navigation("EspecialidadeMedicos");
                });

            modelBuilder.Entity("AloDoutor.Domain.Entity.EspecialidadeMedico", b =>
                {
                    b.Navigation("Agendamentos");
                });

            modelBuilder.Entity("AloDoutor.Domain.Entity.Medico", b =>
                {
                    b.Navigation("EspecialidadesMedicos");
                });

            modelBuilder.Entity("AloDoutor.Domain.Entity.Paciente", b =>
                {
                    b.Navigation("Agendamentos");
                });
#pragma warning restore 612, 618
        }
    }
}
