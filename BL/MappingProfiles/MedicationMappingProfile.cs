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
    public class MedicationMappingProfile : Profile
    {
        public MedicationMappingProfile()
        {
            CreateMap<Medication, MedicationDTO>();
            CreateMap<Medication, MedicationSearchValuesDTO>();
            CreateMap<Medication, MedicationBasicDTO>().ReverseMap();
            CreateMap<CreateMedicationDTO, Medication>();
        }

    }
}
