using BL.DTO;
using HealthCareAppWPF.DTO;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Managers.Interfaces
{
    public interface IPatientManager : IGenericManager<Patient>
    {
        Task<PatientDTO> GetPatientByIdIncludingAddressAsync(int patientId);
        Task<PatientDTO> GetByIdAsync(int patientId);
        public Task<List<PatientBasicDTO>> SearchPatientsAsync(PatientSearchValuesDTO patientQuery);
        Task<PatientDTO> SearchPatientWithAdressAsync(PatientSearchValuesDTO patientQuery);
        void Update(PatientDTO patientDTO);
        bool Add(PatientDTO patientDTO);
        List<string> ValidatePatient(PatientDTO patientDTO);
    }
}
