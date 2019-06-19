using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Shelter.Core.Entities;
using Shelter.Core.Enums;

namespace Shelter.Infrastructure.Data
{
    public class ShelterDbContext : DbContext
    {
        #region Entities

        public DbSet<Animal> Animals { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Sponsorship> Sponsorships { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Treatment> Treatments { get; set; }
        public DbSet<Volunteer> Volunteers { get; set; }

        #endregion

        public ShelterDbContext(DbContextOptions<ShelterDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("sh");

            #region Keys

            modelBuilder.Entity<Animal>()
                .HasKey(nameof(Animal.Id));

            modelBuilder.Entity<Customer>()
                .HasKey(nameof(Customer.Id));

            modelBuilder.Entity<Employee>()
                .HasKey(nameof(Employee.Id));

            modelBuilder.Entity<Sponsorship>()
                .HasKey(nameof(Sponsorship.Id));

            modelBuilder.Entity<Transaction>()
                .HasKey(nameof(Transaction.Id));

            modelBuilder.Entity<Treatment>()
                .HasKey(nameof(Treatment.Id));

            modelBuilder.Entity<Volunteer>()
                .HasKey(nameof(Volunteer.Id));

            #endregion

            #region Enums

            modelBuilder.Entity<Animal>()
                .Property(e => e.Race).HasConversion(
                v => v.ToString(),
                v => (Race) Enum.Parse(typeof(Race), v));

            modelBuilder.Entity<Animal>()
                .Property(e => e.Feature).HasConversion(
                    v => v.ToString(),
                    v => (Feature) Enum.Parse(typeof(Feature), v));

            modelBuilder.Entity<Employee>()
                .Property(e => e.Position).HasConversion(
                v => v.ToString(),
                v => (Position) Enum.Parse(typeof(Position), v));

            modelBuilder.Entity<Transaction>()
                .Property(e => e.Type).HasConversion(
                v => v.ToString(),
                v => (TransactionType) Enum.Parse(typeof(TransactionType), v));

            modelBuilder.Entity<Treatment>()
                .Property(e => e.Disease).HasConversion(
                v => v.ToString(),
                v => (Disease) Enum.Parse(typeof(Disease), v));

            modelBuilder.Entity<Volunteer>()
                .Property(e => e.HelpKind).HasConversion(
                v => v.ToString(),
                v => (HelpKind) Enum.Parse(typeof(HelpKind), v));

            #endregion

            #region Relations

            modelBuilder.Entity<Treatment>()
                .HasOne(uc => uc.Animal)
                .WithMany(ne => ne.Treatments)
                .HasForeignKey(uo => uo.AnimalId)
                .HasPrincipalKey(uo => uo.Id);

            modelBuilder.Entity<Treatment>()
                .HasOne(uc => uc.Employee)
                .WithMany(ne => ne.Treatments)
                .HasForeignKey(uo => uo.EmployeeId)
                .HasPrincipalKey(uo => uo.Id);

            modelBuilder.Entity<Animal>()
                .HasOne(uc => uc.Customer)
                .WithMany(ne => ne.Animals)
                .HasForeignKey(uo => uo.CustomerId)
                .HasPrincipalKey(uo => uo.Id);

            modelBuilder.Entity<Sponsorship>()
                .HasOne(a => a.Volunteer)
                .WithMany(ne => ne.Sponsorships)
                .HasForeignKey(uo => uo.VolunteerId)
                .HasPrincipalKey(uo => uo.Id);

            #endregion
        }
    }
}

