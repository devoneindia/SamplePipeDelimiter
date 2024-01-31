﻿// <auto-generated />
using System;
using LoadDataProject.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LoadDataProject.Migrations
{
    [DbContext(typeof(PubAccDbContext))]
    [Migration("20240131184706_SC_20240201")]
    partial class SC_20240201
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("LoadDataProject.Models.PubAccEM", b =>
                {
                    b.Property<decimal>("UniqueSystemIdentifier")
                        .HasPrecision(10)
                        .HasColumnType("numeric(10,0)")
                        .HasColumnName("unique_system_identifier")
                        .HasColumnOrder(1);

                    b.Property<int?>("AntennaNumber")
                        .HasColumnType("integer")
                        .HasColumnName("antenna_number")
                        .HasColumnOrder(6);

                    b.Property<string>("CallSign")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("call_sign")
                        .HasColumnOrder(4);

                    b.Property<decimal?>("DigitalModRate")
                        .HasPrecision(8, 1)
                        .HasColumnType("numeric(8,1)")
                        .HasColumnName("digital_mod_rate")
                        .HasColumnOrder(10);

                    b.Property<string>("DigitalModType")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("digital_mod_type")
                        .HasColumnOrder(11);

                    b.Property<string>("EbfNumber")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("ebf_number")
                        .HasColumnOrder(3);

                    b.Property<string>("EmissionActionPerformed")
                        .HasMaxLength(1)
                        .HasColumnType("character varying(1)")
                        .HasColumnName("emission_action_performed")
                        .HasColumnOrder(8);

                    b.Property<string>("EmissionCode")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("emission_code")
                        .HasColumnOrder(9);

                    b.Property<int?>("EmissionSequenceId")
                        .HasColumnType("integer")
                        .HasColumnName("emission_sequence_id")
                        .HasColumnOrder(15);

                    b.Property<decimal?>("FrequencyAssigned")
                        .HasPrecision(16, 8)
                        .HasColumnType("numeric(16,8)")
                        .HasColumnName("frequency_assigned")
                        .HasColumnOrder(7);

                    b.Property<int?>("FrequencyNumber")
                        .HasColumnType("integer")
                        .HasColumnName("frequency_number")
                        .HasColumnOrder(12);

                    b.Property<int?>("LocationNumber")
                        .HasColumnType("integer")
                        .HasColumnName("location_number")
                        .HasColumnOrder(5);

                    b.Property<string>("RecordType")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("character varying(2)")
                        .HasColumnName("record_type")
                        .HasColumnOrder(0);

                    b.Property<string>("StatusCode")
                        .HasMaxLength(1)
                        .HasColumnType("character varying(1)")
                        .HasColumnName("status_code")
                        .HasColumnOrder(13);

                    b.Property<DateTime?>("StatusDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("status_date")
                        .HasColumnOrder(14);

                    b.Property<string>("UlsFileNumber")
                        .HasMaxLength(14)
                        .HasColumnType("character varying(14)")
                        .HasColumnName("uls_file_number")
                        .HasColumnOrder(2);

                    b.HasKey("UniqueSystemIdentifier");

                    b.HasIndex("UniqueSystemIdentifier");

                    b.ToTable("pubacc_em", "main");
                });
#pragma warning restore 612, 618
        }
    }
}