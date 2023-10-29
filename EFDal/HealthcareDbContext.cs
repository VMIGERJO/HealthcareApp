using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDal.Data
{
    using EFDal.Data.EntityConfiguration;
    using EFDal.Entities;
    using Microsoft.EntityFrameworkCore;

    public class HealthcareDbContext : DbContext
    {
        public HealthcareDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Medication> Medications { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;" + "Initial Catalog=HealthcareDb;" + "Integrated Security=True;" + "Trusted_Connection=True;" + "TrustServerCertificate=True");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new DoctorEntityTypeConfiguration().Configure(modelBuilder.Entity<Doctor>());
            new PatientEntityTypeConfiguration().Configure(modelBuilder.Entity<Patient>());
            new PrescriptionEntityTypeConfiguration().Configure(modelBuilder.Entity<Prescription>());
            new MedicationEntityTypeConfiguration().Configure(modelBuilder.Entity<Medication>());
        }
    }

}
