using Microsoft.EntityFrameworkCore;
using CleanLand.Data.Models;
using System.Collections.Generic;
using System.Reflection.Emit;
using CleanLand.Controllers.Forest;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace CleanLand.Data.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<Pond> Ponds { get; set; }
        public DbSet<Lessee> Lessees { get; set; }
        public DbSet<LeaseAgreement> LeaseAgreements { get; set; }
        public DbSet<WaterUsagePermit> WaterUsagePermits { get; set; }

        public DbSet<Forest> Forests { get; set; }
        public DbSet<TreeSpecie> TreeSpecies { get; set; }
        public DbSet<AreaData> DeforestationDatas { get; set; }

        public DbSet<User> Users { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUserLogin<string>>()
                .HasKey(l => new { l.LoginProvider, l.ProviderKey });

            // Configure one-to-many relationship: A forest has many deforestation data entries
            modelBuilder.Entity<AreaData>()
                .HasOne<Forest>()
                .WithMany(f => f.AreaDatas)
                .HasForeignKey("ForestId")
                .OnDelete(DeleteBehavior.Cascade);

            // Configure many-to-many relationship between Forest and TreeSpecie
            modelBuilder.Entity<Forest>()
                .HasMany(f => f.TreeSpecies)
                .WithMany();

            // Seed initial data (optional)
            modelBuilder.Entity<Forest>().HasData(
                new Forest
                {
                    Id = 1,
                    Name = "Amazon Rainforest",
                    Description = "One of the world's largest and most diverse forests.",
                    XLocation = -3.4653,
                    YLocation = -62.2159,
                    NGO = "Amazon Conservation Association",
                    IsProtectedByLaw = true,
                    TreesAmount = 390000000000,
                    TonsOfSequesteredToDate = 1000000000,
                    TonsOfSequesteredPotential = 5000000000,
                    AverageYearTemperature = 27.5,
                    FireIncidentsAmount = 50,
                    CriticalityScore = 8.5
                }
            );
        }
    }
}
