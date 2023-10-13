using Les2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Les2.Data.EntityConfiguration
{
    public class PrescriptionMedicationEntityTypeConfiguration : IEntityTypeConfiguration<PrescriptionMedication>
    {
        public void Configure(EntityTypeBuilder<PrescriptionMedication> modelBuilder)
        {
            modelBuilder
               .HasKey(pm => new { pm.PrescriptionID, pm.MedicationID });

            modelBuilder
                .HasOne(pm => pm.Prescription)
                .WithMany(p => p.PrescriptionMedications)
                .HasForeignKey(pm => pm.PrescriptionID);

            modelBuilder
                .HasOne(pm => pm.Medication)
                .WithMany(m => m.PrescriptionMedications)
                .HasForeignKey(pm => pm.MedicationID);
        }
    }
}
