using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTO
{
    public class MedicationDTO
    {
        public int MedicationId { get; set; }
        public string? ActiveSubstance { get; set; }
        public string Name { get; set; }
        public string Dosage { get; set; }
        public string? Manufacturer { get; set; }
        public List<PrescriptionDTO> Prescriptions { get; set; }
    }
}
