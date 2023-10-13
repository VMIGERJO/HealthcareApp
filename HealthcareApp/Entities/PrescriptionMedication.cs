using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Les2.Entities
{
    public class PrescriptionMedication
    {
        public int PrescriptionID { get; set; }
        public int MedicationID { get; set; }
        public Prescription Prescription { get; set; }
        public Medication Medication { get; set; }
    }
}
