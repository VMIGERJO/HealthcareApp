using BL.DTO;
using HealthCareAppWPF.DTO;
using EFDal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Managers.Interfaces
{
    public interface IPrescriptionManager : IGenericManager<Prescription>
    {
        Task<List<PrescriptionViewDTO>> PrescriptionSearchAsync(PrescriptionSearchValuesDTO prescriptionQuery);
        bool Add(PrescriptionDTO prescriptionDTO);
        Task<PrescriptionDTO> GetPrescriptionByIdIncludingMedicationsAsync(int patientId);
    }
}
