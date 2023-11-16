using EFDal.Repositories.Interfaces;
using EFDal.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFDal.Data;
using EFDal.ExtensionMethods;

namespace EFDal.Repositories
{
    public class PrescriptionRepository : GenericRepository<Prescription>, IPrescriptionRepository
    {
        public PrescriptionRepository(HealthcareDbContext context) : base(context)
        {
        }

        public int Insert(Prescription prescription)
        {
            for (int i = 0; i < prescription.Medications.Count; i++)
            {
                prescription.Medications[i] = _context.ProcessStubEntity<Medication>(prescription.Medications[i]);
            }
            return base.Insert(prescription);
        }

    }
}
