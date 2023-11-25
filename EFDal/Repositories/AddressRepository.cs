using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFDal.Data;
using EFDal.Entities;

namespace EFDal.Repositories
{
    public class AddressRepository : GenericRepository<Address>
    {
        public AddressRepository(HealthcareDbContext context) : base(context)
        {
        }
    }
}
