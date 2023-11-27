using BL.Managers.Interfaces;
using EFDal.Repositories.Interfaces;
using EFDal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.DTO;
using EFDal.Repositories;
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

        public async Task<List<PrescriptionViewDTO>> PrescriptionSearchAsync(PrescriptionSearchValuesDTO prescriptionQuery)
        {
            List<Expression<Func<Prescription, bool>>> searchExpression = new();

            if (prescriptionQuery?.PatientID != null)
                searchExpression.Add(p => p.PatientID.Equals(prescriptionQuery.PatientID));

            if (prescriptionQuery?.DoctorID != null)
                searchExpression.Add(p => p.DoctorID.Equals(prescriptionQuery.DoctorID));

            List<Prescription> searchResults = await _prescriptionRepository.SearchPrescriptionsIncludingDoctorPatientMedicationAsync(searchExpression, p => p.PrescriptionDate, false);

            var result = searchResults.Select(pr => new PrescriptionViewDTO()
            {
                PatientName = $"{pr.Patient.LastName} {pr.Patient.FirstName}",
                DoctorName = $"{pr.Doctor.LastName} {pr.Doctor.FirstName}",
                Id = pr.Id,
                Date = pr.PrescriptionDate.ToString("dd/MM/yyyy"),
                MedicationNames = string.Join(", ", pr.Medications?.Select(m => m.Name) ?? Enumerable.Empty<string>())


        }).ToList();

            return result;
        }
    }
}
