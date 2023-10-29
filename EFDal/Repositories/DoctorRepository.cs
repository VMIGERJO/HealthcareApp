using EFDal.Repositories.Interfaces;
using EFDal.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFDal.Data;

namespace EFDal.Repositories
{
    public class DoctorRepository : GenericRepository<Doctor>, IDoctorRepository
    {
        public DoctorRepository(HealthcareDbContext context) : base(context)
        {
        }

        public Doctor GetByName(string firstName, string lastName)
        {
            Doctor result = _dbSet.Where(p => p.FirstName == firstName && p.LastName == lastName).FirstOrDefault();

            return result;
        }
    }
}
