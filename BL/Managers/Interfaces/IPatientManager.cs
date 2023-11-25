﻿using BL.DTO;
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
        Task<Patient> GetPatientByIdIncludingAddressAsync(int patientId);
        public Task<List<PatientBasicDTO>> SearchPatientsAsync(PatientSearchValuesDTO patientQuery);
        Task<Patient> SearchPatientWithAdressAsync(PatientSearchValuesDTO patientQuery);
    }
}
