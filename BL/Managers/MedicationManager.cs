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
    public class MedicationManager : GenericManager<Medication>, IMedicationManager
    {
        internal readonly IMedicationRepository _medicationRepository;
        public MedicationManager(IMapper mapper, IMedicationRepository medicationRepository) : base(mapper, medicationRepository)
        {
            _medicationRepository = medicationRepository;
        }

        public bool Add(CreateMedicationDTO newMedicationDTO)
        {
            Medication medication = Mapper.Map<Medication>(newMedicationDTO);
            return base.Add(medication);
        }
    

        public async Task<List<MedicationBasicDTO>> GetAllMedicationsAsync()
        {
            List<Medication> medications = await _medicationRepository.GetAllAsync();

            List<MedicationBasicDTO> medicationBasicDTOs = Mapper.Map<List<MedicationBasicDTO>>(medications);
            return medicationBasicDTOs;
        }

        public async Task<List<MedicationBasicDTO>> MedicationSearchAsync(MedicationSearchValuesDTO medicationQuery)
        {
            List<Expression<Func<Medication, bool>>> searchExpression = new();

            if (medicationQuery?.Name != null)
                searchExpression.Add(m => m.Name.Contains(medicationQuery.Name));

            List<Medication> searchResults = await _medicationRepository.SearchAsync(searchExpression, m => m.Name);

            List<MedicationBasicDTO> result = Mapper.Map<List<MedicationBasicDTO>>(searchResults);

            return result;
        }
    }
}
