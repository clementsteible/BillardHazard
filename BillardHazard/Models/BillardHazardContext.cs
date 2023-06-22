using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BillardHazard.Models;

public partial class BillardHazardContext : DbContext
{
    public BillardHazardContext()
    {
    }

    public BillardHazardContext(DbContextOptions<BillardHazardContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Bonu> Bonus { get; set; }

    public virtual DbSet<Rule> Rules { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySQL("Server=localhost;Database=billard_hazard;Uid=root;Pwd=root;Port=3306;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bonu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("bonus");

            entity.Property(e => e.Id).HasColumnType("mediumint");
            entity.Property(e => e.Explanation).HasMaxLength(255);
        });

        modelBuilder.Entity<Rule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("rule");

            entity.Property(e => e.Id).HasColumnType("mediumint");
            entity.Property(e => e.Explanation).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
