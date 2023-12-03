using DAL.DapperAttributes;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Prescription : BaseEntity
    {
        public int PatientId { get; set; }
        [Navigation]
        public Patient Patient { get; set; }
        public int DoctorId { get; set; }
        [Navigation]
        public Doctor Doctor { get; set; }
        public DateTime PrescriptionDate { get; set; }
        [Navigation]
        public List<Medication> Medications { get; } = new();
    }

}
