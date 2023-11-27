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
    public class PrescriptionMappingProfile :Profile
    {
        public PrescriptionMappingProfile()
        {
            CreateMap<Prescription, PrescriptionDTO>()
            .ForMember(dest => dest.Medications, opt => opt.MapFrom(src => src.Medications));

            CreateMap<Medication, MedicationDTO>();
            CreateMap<Prescription, PrescriptionSearchValuesDTO>();
            CreateMap<Prescription, PrescriptionViewDTO>();

        }
    }
}
