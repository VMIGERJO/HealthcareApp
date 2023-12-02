using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data
{
    using DAL.Data.EntityConfiguration;
    using DAL.Entities;
    using DAL.EntityConfiguration;
    using Microsoft.EntityFrameworkCore;

    public class HealthcareDbContext : DbContext
    {
        public HealthcareDbContext() : base()
        {
        }

        public HealthcareDbContext(DbContextOptions<HealthcareDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Needed to add this to add migrations... Add migrations only works with empty constructor, but then complains it doesnt get any options
            // Entity Framework doesn't recognize the WPF Dependency Injection -> keeps looking for static method 'CreateHostBuilder(string[])'
            // -> only exists in ASP.NET Core
            // Yes hardcoded, but alternative is referencing my WPF project settings from the DAL project...
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=HealthcareDb;Integrated Security=True;TrustServerCertificate=True;");
            }
        }



        public DbSet<Medication> Medications { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Address> Address { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new DoctorEntityTypeConfiguration().Configure(modelBuilder.Entity<Doctor>());
            new PatientEntityTypeConfiguration().Configure(modelBuilder.Entity<Patient>());
            new PrescriptionEntityTypeConfiguration().Configure(modelBuilder.Entity<Prescription>());
            new MedicationEntityTypeConfiguration().Configure(modelBuilder.Entity<Medication>());
            new AddressEntityTypeConfiguration().Configure(modelBuilder.Entity<Address>());
        }
    }

}
