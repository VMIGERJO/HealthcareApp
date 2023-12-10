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
            CreateMap<Prescription, PrescriptionViewDTO>()
                .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => $"{src.Patient.LastName} {src.Patient.FirstName}"))
                .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => $"{src.Doctor.LastName} {src.Doctor.FirstName}"))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.PrescriptionDate.ToString("dd/MM/yyyy")))
                .ForMember(dest => dest.MedicationNames, opt => opt.MapFrom(src => string.Join(", ", src.Medications.Select(m => m.Name))));

            CreateMap<Medication, MedicationDTO>()
                .ForMember(dest => dest.Prescriptions, opt => opt.MapFrom(src => src.Prescriptions))
                .ReverseMap();

        }
    }
}
