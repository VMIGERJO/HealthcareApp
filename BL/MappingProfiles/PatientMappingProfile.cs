using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using BL.DTO;

namespace BL.MappingProfiles
{
    public class PatientMappingProfile : Profile
    {
        public PatientMappingProfile()
        {
            CreateMap<Address, AddressDTO>().ReverseMap();
            CreateMap<Patient, PatientDTO>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.Prescriptions, opt => opt.MapFrom(src => src.Prescriptions)).ReverseMap();
            CreateMap<Patient, PatientSearchValuesDTO>();
        }
    }
}
