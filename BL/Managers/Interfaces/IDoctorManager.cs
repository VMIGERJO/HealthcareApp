﻿using BL.DTO;
using DAL.Entities;
using HealthCareAppWPF.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Managers.Interfaces
{
    public interface IDoctorManager : IGenericManager<Doctor>
    {
        Task<List<DoctorBasicDTO>> DoctorSearchAsync(DoctorSearchValuesDTO doctorQuery);
        Task<List<DoctorBasicDTO>> GetAllDoctorsAsync();
        Task<DoctorDTO> UniqueDoctorSearchAsync(DoctorSearchValuesDTO doctorQuery);
        void Update(DoctorDTO doctorDTO);
        bool Add(DoctorDTO doctorDTO);
        
    }
}
