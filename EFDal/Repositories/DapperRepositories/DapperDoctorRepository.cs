using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories.DapperRepositories
{
    public class DapperDoctorRepository : DapperGenericRepository<Doctor>, IDoctorRepository
    {
        public DapperDoctorRepository(DbConnectionFactory dbConnectionFactory) : base(dbConnectionFactory)
        {
        }
    }
}
