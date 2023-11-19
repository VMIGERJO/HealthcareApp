using EFDal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTO
{
    public class PrescriptionSearchValuesDTO
    {
        public int? PatientID { get; set; }
        public int? DoctorID { get; set; }
    }
}
