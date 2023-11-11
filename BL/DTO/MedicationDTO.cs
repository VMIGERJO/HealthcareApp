using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTO
{
    public class MedicationDTO
    {
        public string MedicationName { get; set; }
        public string Dose { get; set; }
        public bool IsSelected { get; set; }
    }
}
