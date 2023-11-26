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
using System.Linq.Expressions;

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
                prescription.Medications[i] = _context.ToTracked(prescription.Medications[i]);
            }
            return base.Insert(prescription);
        }

        public async Task<List<Prescription>> SearchPrescriptionsIncludingDoctorPatientMedicationAsync(List<Expression<Func<Prescription, bool>>> searchExpression, Expression<Func<Prescription, object>> orderExpression, bool orderAsc)
        {
            return await base.SearchAsync(searchExpression, orderExpression, orderAsc, p => p.Doctor, p => p.Patient, p => p.Medications);
        }
    }
}
