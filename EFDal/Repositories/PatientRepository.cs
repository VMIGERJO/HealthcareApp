using EFDal.Repositories.Interfaces;
using Les2.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDal.Repositories
{
    public class PatientRepository : GenericRepository<Patient>, IPatientRepository
    {
        public PatientRepository(DbContext context) : base(context)
        {
        }

        public Patient GetByName(string firstName, string lastName)
        {
            Patient result = _dbSet.Where(p => p.FirstName == firstName && p.LastName == lastName).FirstOrDefault();

            return result;
        }
    }
}
