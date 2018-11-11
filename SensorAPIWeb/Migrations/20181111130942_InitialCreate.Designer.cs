﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SensorAPIWeb.Domain;

namespace SensorAPIWeb.Migrations
{
    [DbContext(typeof(SensorAPIDbContext))]
    [Migration("20181111130942_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SensorAPIWeb.Domain.Entities.Device", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BatteryVoltage");

                    b.Property<string>("FirmwareVersion");

                    b.Property<string>("SerialNumber")
                        .IsRequired()
                        .HasMaxLength(24);

                    b.Property<DateTime>("UpdateTime");

                    b.HasKey("Id");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("SensorAPIWeb.Domain.Entities.DeviceDetails", b =>
                {
                    b.Property<int>("DeviceDetailsId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("DeviceId");

                    b.Property<string>("Humidity");

                    b.Property<string>("SerialNumber");

                    b.Property<string>("Temperature");

                    b.Property<DateTime>("UpdateTime");

                    b.HasKey("DeviceDetailsId");

                    b.HasIndex("DeviceId");

                    b.ToTable("DeviceDetails");
                });

            modelBuilder.Entity("SensorAPIWeb.Domain.Entities.Token", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("TokenNumber")
                        .HasMaxLength(64);

                    b.Property<int>("TypeId");

                    b.Property<DateTime>("UpdateTime");

                    b.HasKey("Id");

                    b.HasIndex("TypeId")
                        .IsUnique();

                    b.ToTable("Tokens");
                });

            modelBuilder.Entity("SensorAPIWeb.Domain.Entities.DeviceDetails", b =>
                {
                    b.HasOne("SensorAPIWeb.Domain.Entities.Device", "Device")
                        .WithMany("DeviceDetails")
                        .HasForeignKey("DeviceId");
                });

            modelBuilder.Entity("SensorAPIWeb.Domain.Entities.Token", b =>
                {
                    b.HasOne("SensorAPIWeb.Domain.Entities.Device", "Device")
                        .WithOne("Token")
                        .HasForeignKey("SensorAPIWeb.Domain.Entities.Token", "TypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
