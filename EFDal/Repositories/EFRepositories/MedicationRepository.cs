using DAL.Repositories.Interfaces;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Data;

namespace DAL.Repositories
{
    public class MedicationRepository : GenericRepository<Medication>, IMedicationRepository
    {
        public MedicationRepository(HealthcareDbContext context) : base(context)
        {
        }

    }
}
