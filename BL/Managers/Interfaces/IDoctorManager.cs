using Les2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Managers.Interfaces
{
    public interface IDoctorManager : IGenericManager<Doctor>
    {
        public Doctor GetByName(string firstName, string lastName);
    }
}
