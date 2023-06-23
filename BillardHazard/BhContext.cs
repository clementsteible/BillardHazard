using BillardHazard.Models;
using Microsoft.EntityFrameworkCore;

namespace BillardHazard
{
    public class BhContext : DbContext
    {
        public BhContext(DbContextOptions<BhContext> options) : base(options) { }
        public BhContext() { }

        public DbSet<Rule> Rules {get; set; }
        public DbSet<Bonus> Bonuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rule>().ToTable("Rule");
            modelBuilder.Entity<Bonus>().ToTable("Bonus");
        }
    }
}
