using DAL.DapperAttributes;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Medication : BaseEntity
    {
        public string? ActiveSubstance { get; set; }
        public string Name { get; set; }
        public String Dosage { get; set; }
        public string? Manufacturer { get; set; }
        [Navigation]
        public List<Prescription> Prescriptions { get; } = new();
    }

}
