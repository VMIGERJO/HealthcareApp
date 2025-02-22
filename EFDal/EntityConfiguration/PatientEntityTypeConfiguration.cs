﻿using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data.EntityConfiguration
{
    public class PatientEntityTypeConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder
               .Property(p => p.FirstName)
               .IsRequired()
            .HasMaxLength(40);

            builder
               .Property(p => p.LastName)
               .IsRequired()
            .HasMaxLength(40);

            builder
               .HasOne(p => p.Address).WithMany(a => a.Patients).HasForeignKey(p => p.AddressId);

            builder
               .Property(p => p.Age)
            .IsRequired();

            builder
               .Property(p => p.MedicalHistory)
               .HasMaxLength(300);
        }
    }
}
