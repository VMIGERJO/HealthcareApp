using Les2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Managers.Interfaces
{
    public interface IPatientManager : IGenericManager<Patient>
    {
        public Patient GetByName(string firstName, string lastName);
    }
}
