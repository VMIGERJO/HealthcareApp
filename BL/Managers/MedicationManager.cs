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

namespace BL.Managers
{
    public class MedicationManager : GenericManager<Medication>, IMedicationManager
    {
        internal readonly IMedicationRepository _medicationRepository;
        public MedicationManager(IMedicationRepository medicationRepository) : base(medicationRepository)
        {
            _medicationRepository = medicationRepository;
        }

        public bool Add(CreateMedicationDTO newMedicationDTO)
        {
            Medication medication = new()
            {
                Name = newMedicationDTO.Name ?? throw new ArgumentNullException(nameof(newMedicationDTO.Name), "The required field cannot be null."),
                Dosage = newMedicationDTO.Dosage ?? throw new ArgumentNullException(nameof(newMedicationDTO.Name), "The required field cannot be null."),
                Manufacturer = newMedicationDTO.Manufacturer,
                ActiveSubstance = newMedicationDTO.ActiveSubstance

            };

            return base.Add(medication);

        }
    

        public async Task<List<MedicationBasicDTO>> GetAllMedicationsAsync()
        {
            var searchResults = await _medicationRepository.GetAllAsync();

            var result = searchResults.Select(md => new MedicationBasicDTO()
            {
                MedicationName = md.Name,
                Dose = md.Dosage,
                Id = md.Id
            }).ToList();

            return result;
        }

        public async Task<List<MedicationBasicDTO>> MedicationSearchAsync(MedicationSearchValuesDTO medicationQuery)
        {
            List<Expression<Func<Medication, bool>>> searchExpression = new();

            if (medicationQuery?.Name != null)
                searchExpression.Add(m => m.Name.Contains(medicationQuery.Name));

            List<Medication> searchResults = await _repository.SearchAsync(searchExpression, m => m.Name);

            List<MedicationBasicDTO> result = searchResults.Select(m => new MedicationBasicDTO()
            {
                MedicationName = m.Name,
                Id = m.Id,
                Dose = m.Dosage
            }).ToList();

            return result;
        }
    }
}
