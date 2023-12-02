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
    public class PrescriptionMappingProfile :Profile
    {
        public PrescriptionMappingProfile()
        {
            CreateMap<Prescription, PrescriptionDTO>()
            .ForMember(dest => dest.Medications, opt => opt.MapFrom(src => src.Medications))
            .ReverseMap();

            CreateMap<Medication, MedicationBasicDTO>().ReverseMap();
            CreateMap<Prescription, PrescriptionSearchValuesDTO>();
            CreateMap<Prescription, PrescriptionViewDTO>();

            CreateMap<Medication, MedicationDTO>()
                .ForMember(dest => dest.Prescriptions, opt => opt.MapFrom(src => src.Prescriptions))
                .ReverseMap();

        }
    }
}
