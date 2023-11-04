using BL.DTO;
using EFDal.Entities;
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
        List<DoctorBasicDTO> DoctorSearch(DoctorSearchValuesDTO doctorQuery);
        Doctor UniqueDoctorSearch(DoctorSearchValuesDTO doctorQuery);
        
    }
}
