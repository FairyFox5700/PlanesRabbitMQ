﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PlanesRabbitMQ.DAL;

namespace PlanesRabbitMQ.DAL.Migrations
{
    [DbContext(typeof(PlanesContext))]
    [Migration("20211119105130_Update")]
    partial class Update
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PlanesRabbitMQ.DAL.Entities.Chars", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CharacteristicsId")
                        .HasColumnType("int");

                    b.Property<bool>("HasAmmunition")
                        .HasColumnType("bit");

                    b.Property<bool>("HasRadar")
                        .HasColumnType("bit");

                    b.Property<int>("NumberOfSeats")
                        .HasColumnType("int");

                    b.Property<int?>("ScoutCharacteristicsId")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CharacteristicsId");

                    b.HasIndex("ScoutCharacteristicsId");

                    b.ToTable("Chars");
                });

            modelBuilder.Entity("PlanesRabbitMQ.DAL.Entities.Parameters", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Height")
                        .HasColumnType("float");

                    b.Property<double>("Length")
                        .HasColumnType("float");

                    b.Property<double>("Width")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Parameters");
                });

            modelBuilder.Entity("PlanesRabbitMQ.DAL.Entities.ParametersCharacteristics", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Height")
                        .HasColumnType("float");

                    b.Property<double>("Length")
                        .HasColumnType("float");

                    b.Property<double>("Width")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("ParametersCharacteristics");
                });

            modelBuilder.Entity("PlanesRabbitMQ.DAL.Entities.Plane", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CharsId")
                        .HasColumnType("int");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Origin")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ParametersId")
                        .HasColumnType("int");

                    b.Property<int?>("ParametersId1")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("CharsId");

                    b.HasIndex("ParametersId");

                    b.HasIndex("ParametersId1");

                    b.ToTable("Planes");
                });

            modelBuilder.Entity("PlanesRabbitMQ.DAL.Entities.ScoutCharacteristics", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("RocketsCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ScoutCharacteristics");
                });

            modelBuilder.Entity("PlanesRabbitMQ.DAL.Entities.Chars", b =>
                {
                    b.HasOne("PlanesRabbitMQ.DAL.Entities.ScoutCharacteristics", "Characteristics")
                        .WithMany()
                        .HasForeignKey("CharacteristicsId");

                    b.HasOne("PlanesRabbitMQ.DAL.Entities.ScoutCharacteristics", null)
                        .WithMany()
                        .HasForeignKey("ScoutCharacteristicsId");

                    b.Navigation("Characteristics");
                });

            modelBuilder.Entity("PlanesRabbitMQ.DAL.Entities.Plane", b =>
                {
                    b.HasOne("PlanesRabbitMQ.DAL.Entities.Chars", "Chars")
                        .WithMany()
                        .HasForeignKey("CharsId");

                    b.HasOne("PlanesRabbitMQ.DAL.Entities.Parameters", "Parameters")
                        .WithMany()
                        .HasForeignKey("ParametersId");

                    b.HasOne("PlanesRabbitMQ.DAL.Entities.Parameters", null)
                        .WithMany()
                        .HasForeignKey("ParametersId1");

                    b.Navigation("Chars");

                    b.Navigation("Parameters");
                });
#pragma warning restore 612, 618
        }
    }
}
