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
    public class MedicationRepository : GenericRepository<Medication>, IMedicationRepository
    {
        public MedicationRepository(DbContext context) : base(context)
        {
        }

        public Medication GetByTradeNameAndDosage(string tradeName, string dosage)
        {
            Medication result = _dbSet.Where(m => m.Name == tradeName && m.Dosage == dosage).FirstOrDefault();

            return result;
        }
    }
}
