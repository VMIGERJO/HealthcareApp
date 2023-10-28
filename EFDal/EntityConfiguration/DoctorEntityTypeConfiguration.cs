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
    public class DoctorEntityTypeConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {

            builder
                .Property(d => d.FirstName)
                .IsRequired()
            .HasMaxLength(20);

            builder
                .Property(d => d.LastName)
                .IsRequired()
            .HasMaxLength(40);

            builder
                .Property(d => d.Specialization)
                .HasMaxLength(20);
        }
    }
}
