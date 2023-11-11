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

namespace BL.Managers
{
    public class MedicationManager : GenericManager<Medication>, IMedicationManager
    {
        internal readonly IMedicationRepository _medicationRepository;
        public MedicationManager(IMedicationRepository medicationRepository) : base(medicationRepository)
        {
            _medicationRepository = medicationRepository;
        }

        public Medication GetByTradeNameAndDosage(string tradeName, string dosage)
        {
            return _medicationRepository.GetByTradeNameAndDosage(tradeName, dosage);
        }

        public async Task<List<MedicationDTO>> GetAllMedicationsAsync()
        {
            var searchResults = await _medicationRepository.GetAllAsync();

            var result = searchResults.Select(md => new MedicationDTO()
            {
                MedicationName = md.Name,
                Dose = md.Dosage,
                IsSelected = false
            }).ToList();

            return result;
        }
    }
}
