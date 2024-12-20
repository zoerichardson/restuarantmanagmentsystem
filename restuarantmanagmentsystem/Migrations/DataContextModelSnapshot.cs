﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using restuarantmanagmentsystem.Data;

#nullable disable

namespace restuarantmanagmentsystem.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("restuarantmanagmentsystem.Models.Menu", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<int>("CategoryType")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.HasKey("ID");

                    b.ToTable("Menu");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            CategoryType = 2,
                            Name = "CheeseBurger",
                            Price = 8f
                        },
                        new
                        {
                            ID = 2,
                            CategoryType = 2,
                            Name = "Pizza",
                            Price = 6f
                        },
                        new
                        {
                            ID = 3,
                            CategoryType = 3,
                            Name = "Ice Cream",
                            Price = 3f
                        });
                });

            modelBuilder.Entity("restuarantmanagmentsystem.Models.Order", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Items")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("StaffID")
                        .HasColumnType("int");

                    b.Property<int?>("TableId")
                        .HasColumnType("int");

                    b.Property<int?>("TableNumber")
                        .HasColumnType("int");

                    b.Property<float>("Total")
                        .HasColumnType("real");

                    b.HasKey("ID");

                    b.HasIndex("StaffID");

                    b.HasIndex("TableId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("restuarantmanagmentsystem.Models.Revenue", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Day")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("DayTotal")
                        .HasColumnType("real");

                    b.HasKey("ID");

                    b.ToTable("Revenue");
                });

            modelBuilder.Entity("restuarantmanagmentsystem.Models.Staff", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Passcode")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Staff");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "George",
                            Passcode = 6060
                        },
                        new
                        {
                            Id = 2,
                            Name = "Lisa",
                            Passcode = 9000
                        },
                        new
                        {
                            Id = 3,
                            Name = "Tom",
                            Passcode = 2222
                        });
                });

            modelBuilder.Entity("restuarantmanagmentsystem.Models.Table", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<int?>("StaffId")
                        .HasColumnType("int");

                    b.Property<int>("TableNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StaffId");

                    b.ToTable("Table");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsAvailable = true,
                            TableNumber = 100
                        },
                        new
                        {
                            Id = 2,
                            IsAvailable = true,
                            TableNumber = 101
                        },
                        new
                        {
                            Id = 3,
                            IsAvailable = true,
                            TableNumber = 103
                        });
                });

            modelBuilder.Entity("restuarantmanagmentsystem.Models.Order", b =>
                {
                    b.HasOne("restuarantmanagmentsystem.Models.Staff", "Staff")
                        .WithMany()
                        .HasForeignKey("StaffID");

                    b.HasOne("restuarantmanagmentsystem.Models.Table", "Table")
                        .WithMany("Orders")
                        .HasForeignKey("TableId");

                    b.Navigation("Staff");

                    b.Navigation("Table");
                });

            modelBuilder.Entity("restuarantmanagmentsystem.Models.Table", b =>
                {
                    b.HasOne("restuarantmanagmentsystem.Models.Staff", null)
                        .WithMany("Tables")
                        .HasForeignKey("StaffId");
                });

            modelBuilder.Entity("restuarantmanagmentsystem.Models.Staff", b =>
                {
                    b.Navigation("Tables");
                });

            modelBuilder.Entity("restuarantmanagmentsystem.Models.Table", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
