using HealthcareApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Les2.Entities
{
    public class Medication : BaseEntity
    {
        public string? ActiveSubstance { get; set; }
        public string Name { get; set; }
        public float? Dosage { get; set; }
        public string? Manufacturer { get; set; }

        public ICollection<PrescriptionMedication> PrescriptionMedications { get; set; }
    }

}
