using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Les2.Data
{
    using Les2.Data.EntityConfiguration;
    using Les2.Entities;
    using Microsoft.EntityFrameworkCore;

    public class HealthcareDbContext : DbContext
    {
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
            new PrescriptionMedicationEntityTypeConfiguration().Configure(modelBuilder.Entity<PrescriptionMedication>());
        }
    }

}
