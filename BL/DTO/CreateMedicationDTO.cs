using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTO
{
    public class CreateMedicationDTO
    {
        public string? ActiveSubstance { get; set; }
        public string Name { get; set; }
        public string Dosage { get; set; }
        public string? Manufacturer { get; set; }
    }
}
