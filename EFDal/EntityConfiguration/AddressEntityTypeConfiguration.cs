using DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EntityConfiguration
{
    internal class AddressEntityTypeConfiguration
    {
            public void Configure(EntityTypeBuilder<Address> builder)
            {

                builder
                    .Property(a => a.Street)
                    .IsRequired()
                .HasMaxLength(40);

                builder
                    .Property(a => a.HouseNumber)
                    .IsRequired()
                .HasMaxLength(10);

                builder
                    .Property(a => a.Appartment)
                .HasMaxLength(10);

                builder
                    .Property(a => a.City)
                    .IsRequired()
                    .HasMaxLength(20);
                builder
                    .Property(a => a.PostalCode)
                    .IsRequired()
                    .HasMaxLength(20);
                builder
                    .Property(a => a.Country)
                    .IsRequired()
                    .HasMaxLength(20);


        }
        }
    }

