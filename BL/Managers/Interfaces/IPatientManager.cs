using BL.DTO;
using HealthCareAppWPF.DTO;
using EFDal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Managers.Interfaces
{
    public interface IPatientManager : IGenericManager<Patient>
    {
        public List<PatientBasicDTO> PatientSearch(PatientSearchValuesDTO patientQuery);
        Patient UniquePatientSearch(PatientSearchValuesDTO patientQuery);
    }
}
