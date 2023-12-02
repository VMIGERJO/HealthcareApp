using DAL.Repositories.Interfaces;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Data;
using System.Linq.Expressions;

namespace DAL.Repositories.EFRepositories
{
    public class PatientRepository : GenericRepository<Patient>, IPatientRepository
    {
        public PatientRepository(HealthcareDbContext context) : base(context)
        {
        }

        public async Task<Patient> GetPatientByIdIncludingAddressAsync(int patientId)
        {
            return await _dbSet.AsNoTracking()
                .Include(p => p.Address)
                .FirstOrDefaultAsync(p => p.Id == patientId);
        }

        public async Task<Patient> SearchPatientWithAddressAsync(List<Expression<Func<Patient, bool>>> searchExpression)
        {
            return await base.SearchUniqueAsync(searchExpression, p => p.Address);
        }
    }
}
