using BL.DTO;
using EFDal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Managers.Interfaces
{
    public interface IMedicationManager : IGenericManager<Medication>
    {
        Task<List<MedicationDTO>> GetAllMedicationsAsync();
        public Medication GetByTradeNameAndDosage(string tradeName, string Dosage);
    }
}
