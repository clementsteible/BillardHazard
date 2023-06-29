﻿// <auto-generated />
using System;
using BillardHazard;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BillardHazard.Migrations
{
    [DbContext(typeof(BhContext))]
    [Migration("20230626101350_RemoveEntitiesName")]
    partial class RemoveEntitiesName
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("BillardHazard.Models.Bonus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Explanation")
                        .HasColumnType("longtext");

                    b.Property<int?>("Points")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Bonus", (string)null);
                });

            modelBuilder.Entity("BillardHazard.Models.Rule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Explanation")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Rule", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
