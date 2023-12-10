using DAL.Entities;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.DapperRepositories
{
    public class DapperMedicationRepository : DapperGenericRepository<Medication>, IMedicationRepository
    {
        public DapperMedicationRepository(DbConnectionFactory dbConnectionFactory) : base(dbConnectionFactory)
        {
        }
    }
}
