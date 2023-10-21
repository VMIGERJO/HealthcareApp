using Les2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareApp.Controller
{
    public class DomainController
    {
        private GenericRepository<Doctor> doctorRepository;
        private GenericRepository<Medication> medicationRepository;
        private GenericRepository<Patient> patientRepository;
        private GenericRepository<Prescription> prescriptionRepository;

        public DomainController(GenericRepository<Doctor> doctorRepository, GenericRepository<Medication> medicationRepository, GenericRepository<Patient> patientRepository, GenericRepository<Prescription> prescriptionRepository)
        {
            this.doctorRepository = doctorRepository ?? throw new ArgumentNullException(nameof(doctorRepository));
            this.medicationRepository = medicationRepository ?? throw new ArgumentNullException(nameof(medicationRepository));
            this.patientRepository = patientRepository ?? throw new ArgumentNullException(nameof(patientRepository));
            this.prescriptionRepository = prescriptionRepository ?? throw new ArgumentNullException(nameof(prescriptionRepository));
        }

        public void AddMedication(Medication medication)
        {
            medicationRepository.Insert(medication);
        }

        public void AddPrescription(Prescription prescription)
        {
            prescriptionRepository.Insert(prescription);
        }

        public void AddPatient(Patient patient)
        {
            patientRepository.Insert(patient);
        }

        public void AddDoctor(Doctor doctor)
        {
            doctorRepository.Insert(doctor);
        }

        internal Doctor GetDoctor(string string1, string string2)
        {
            throw new NotImplementedException();
        }

        internal Patient GetPatient(string patientFirstName, string patientLastName)
        {
            throw new NotImplementedException();
        }

        internal Medication GetMedication(string? medicationTradeName, string? v)
        {
            throw new NotImplementedException();
        }
    }
}
