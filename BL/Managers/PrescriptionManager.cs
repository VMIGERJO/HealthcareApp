using BL.Managers.Interfaces;
using DAL.Repositories.Interfaces;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.DTO;
using DAL.Repositories;
using HealthCareAppWPF.DTO;
using System.Linq.Expressions;
using AutoMapper;

namespace BL.Managers
{
    public class PrescriptionManager : GenericManager<Prescription>, IPrescriptionManager
    {
        internal readonly IPrescriptionRepository _prescriptionRepository;
        public PrescriptionManager(IMapper mapper, IPrescriptionRepository prescriptionRepository) : base(mapper, prescriptionRepository)
        {
            _prescriptionRepository = prescriptionRepository;
        }

        public bool Add(PrescriptionDTO prescriptionDTO)
        {
            Prescription newPrescription = Mapper.Map<Prescription>(prescriptionDTO);
            return base.Add(newPrescription);

        }

        public async Task<List<PrescriptionViewDTO>> PrescriptionSearchAsync(PrescriptionSearchValuesDTO prescriptionQuery)
        {
            List<Expression<Func<Prescription, bool>>> searchExpression = new();

            if (prescriptionQuery?.PatientID != null)
            {
                int patientFilter = (int)prescriptionQuery.PatientID; 
                searchExpression.Add(p => p.PatientId == patientFilter);
            }

            if (prescriptionQuery?.DoctorID != null)
            {
                int doctorFilter = (int)prescriptionQuery.DoctorID;
                searchExpression.Add(p => p.DoctorId == doctorFilter);
            }
            List<Prescription> searchResults = await _prescriptionRepository.SearchPrescriptionsIncludingDoctorPatientMedicationAsync(searchExpression, p => p.PrescriptionDate, false);

            List <PrescriptionViewDTO> prescriptionViewDTOs = Mapper.Map<List<PrescriptionViewDTO>>(searchResults);

            return prescriptionViewDTOs;
        }

        public async Task<PrescriptionDTO> GetPrescriptionByIdIncludingMedicationsAsync(int patientId)
        {
            Prescription prescription = await base.GetByIdAsync(patientId, p => p.Medications);
            return Mapper.Map<PrescriptionDTO>(prescription);
        }
    }
}
