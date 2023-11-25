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
        public Task<List<PatientBasicDTO>> PatientSearchAsync(PatientSearchValuesDTO patientQuery);
        Task<Patient> UniquePatientSearchAsync(PatientSearchValuesDTO patientQuery);
    }
}
