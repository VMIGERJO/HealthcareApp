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
    public class MedicationEntityTypeConfiguration : IEntityTypeConfiguration<Medication>
    {
        public void Configure(EntityTypeBuilder<Medication> builder)
        {
            builder
               .Property(m => m.Name)
               .IsRequired()
            .HasMaxLength(20);

            builder
                .Property(m => m.ActiveSubstance)
            .HasMaxLength(40);

            builder
                .Property(m => m.Dosage)
                .HasMaxLength(40);
        }
    }
}
