using EFDal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDal.Entities
{
    public class Medication : BaseEntity
    {
        public string? ActiveSubstance { get; set; }
        public string Name { get; set; }
        public String Dosage { get; set; }
        public string? Manufacturer { get; set; }

        public List<Prescription> Prescriptions { get; } = new();
    }

}
