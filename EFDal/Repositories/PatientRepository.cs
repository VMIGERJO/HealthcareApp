using EFDal.Repositories.Interfaces;
using EFDal.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFDal.Data;
using System.Linq.Expressions;

namespace EFDal.Repositories
{
    public class PatientRepository : GenericRepository<Patient>, IPatientRepository
    {
        public PatientRepository(HealthcareDbContext context) : base(context)
        {
        }

        public async Task<Patient> SearchPatientWithAddressAsync(List<Expression<Func<Patient, bool>>> searchExpression)
        {
            return await base.SearchUniqueAsync(searchExpression, p => p.Address);
        }
    }
}
