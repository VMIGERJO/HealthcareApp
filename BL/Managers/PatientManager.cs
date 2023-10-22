using BL.Managers.Interfaces;
using EFDal.Repositories.Interfaces;
using Les2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Managers
{
    public class PatientManager : GenericManager<Patient>, IPatientManager
    {
        internal readonly IPatientRepository _patientRepository;
        public PatientManager(IPatientRepository patientRepository) : base(patientRepository)
        {
            this._patientRepository = patientRepository;
        } 

        public Patient GetByName(string firstName, string lastName)
        {
           return _patientRepository.GetByName(firstName, lastName);
        }
    }
}
