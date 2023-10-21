﻿using HealthcareApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Les2.Entities
{
    public class Prescription : BaseEntity
    {
        public int PatientID { get; set; }
        public Patient Patient { get; set; }
        public int DoctorID { get; set; }
        public Doctor Doctor { get; set; }
        public DateTime PrescriptionDate { get; set; }
        public List<Medication> Medications { get; } = new();
    }

}
