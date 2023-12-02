using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Data;
using DAL.Entities;

namespace DAL.Repositories
{
    public class AddressRepository : GenericRepository<Address>
    {
        public AddressRepository(HealthcareDbContext context) : base(context)
        {
        }
    }
}
