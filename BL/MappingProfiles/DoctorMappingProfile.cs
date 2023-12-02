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
    public class DoctorMappingProfile : Profile
    {
        public DoctorMappingProfile()
        {
            CreateMap<Doctor, DoctorDTO>().ForMember(dest => dest.Prescriptions, opt => opt.MapFrom(src => src.Prescriptions)).ReverseMap();
            CreateMap<Doctor, DoctorSearchValuesDTO>();
            CreateMap<Doctor, DoctorBasicDTO>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.LastName} {src.FirstName}"));

        }
    }
}
