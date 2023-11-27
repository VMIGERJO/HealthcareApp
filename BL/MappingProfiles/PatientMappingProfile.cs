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
    public class PatientMappingProfile : Profile
    {
        public PatientMappingProfile()
        {
            CreateMap<Patient, PatientDTO>().ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address)).ForMember(dest => dest.Prescriptions, opt => opt.MapFrom(src => src.Prescriptions));
            CreateMap<Patient, PatientSearchValuesDTO>();

            CreateMap<Address, AddressDTO>();
            // CreateMap<Prescription, PrescriptionDTO>();
            // CreateMap<Medication, MedicationDTO>();
        }
    }
}
