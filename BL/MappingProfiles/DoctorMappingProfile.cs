using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFDal.Entities;
using BL.DTO;

namespace BL.MappingProfiles
{
    public class DoctorMappingProfile : Profile
    {
        public DoctorMappingProfile()
        {
            CreateMap<Doctor, DoctorDTO>().ForMember(dest => dest.Prescriptions, opt => opt.MapFrom(src => src.Prescriptions));
            CreateMap<Doctor, DoctorSearchValuesDTO>();

            CreateMap<Prescription, PrescriptionDTO>();
            CreateMap<Medication, MedicationDTO>();
        }
    }
}
