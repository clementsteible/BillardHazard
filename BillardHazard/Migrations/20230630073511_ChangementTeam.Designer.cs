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
    [Migration("20230630073511_ChangementTeam")]
    partial class ChangementTeam
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("BillardHazard.Models.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Game", (string)null);
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

            modelBuilder.Entity("BillardHazard.Models.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Color")
                        .HasColumnType("longtext");

                    b.Property<int?>("GameId")
                        .HasColumnType("int");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("Team", (string)null);
                });

            modelBuilder.Entity("BillardHazard.Models.Team", b =>
                {
                    b.HasOne("BillardHazard.Models.Game", null)
                        .WithMany("Teams")
                        .HasForeignKey("GameId");
                });

            modelBuilder.Entity("BillardHazard.Models.Game", b =>
                {
                    b.Navigation("Teams");
                });
#pragma warning restore 612, 618
        }
    }
}
