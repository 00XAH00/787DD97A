﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using _787DD97A_API.Classes;

#nullable disable

namespace _787DD97A_API.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20221106012720_DeviceIdNotUnique")]
    partial class DeviceIdNotUnique
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("_787DD97A_API.Models.Apartment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Adress")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<uint>("Apartment_floor")
                        .HasColumnType("int unsigned");

                    b.Property<uint>("Apatments_area")
                        .HasColumnType("int unsigned");

                    b.Property<bool>("Balcony")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Condition")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<uint>("House_floors")
                        .HasColumnType("int unsigned");

                    b.Property<uint>("Kitchen_area")
                        .HasColumnType("int unsigned");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<uint>("Material")
                        .HasColumnType("int unsigned");

                    b.Property<ulong>("Price")
                        .HasColumnType("bigint unsigned");

                    b.Property<uint>("Rooms")
                        .HasColumnType("int unsigned");

                    b.Property<uint>("Segment")
                        .HasColumnType("int unsigned");

                    b.Property<string>("Undeground")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<uint>("Undeground_minutes")
                        .HasColumnType("int unsigned");

                    b.HasKey("Id");

                    b.HasIndex("Link")
                        .IsUnique();

                    b.ToTable("Apartments");
                });

            modelBuilder.Entity("_787DD97A_API.Models.User", b =>
                {
                    b.Property<Guid>("uuid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Email")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("FirstName")
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Password")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("SecondName")
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.HasKey("uuid");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("_787DD97A_API.Models.UserDevice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("DeviceId")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RefreshToken")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("TokenCreate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("TokenExpire")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("Useruuid")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("Useruuid");

                    b.ToTable("UserDevices");
                });

            modelBuilder.Entity("_787DD97A_API.Models.UserDevice", b =>
                {
                    b.HasOne("_787DD97A_API.Models.User", null)
                        .WithMany("UsersDevices")
                        .HasForeignKey("Useruuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("_787DD97A_API.Models.User", b =>
                {
                    b.Navigation("UsersDevices");
                });
#pragma warning restore 612, 618
        }
    }
}
