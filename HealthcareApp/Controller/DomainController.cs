using HealthcareApp.Repositories;
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
        private DoctorRepository doctorRepository;
        private MedicationRepository medicationRepository;
        private PatientRepository patientRepository;
        private GenericRepository<Prescription> prescriptionRepository;

        public DomainController(DoctorRepository doctorRepository, MedicationRepository medicationRepository, PatientRepository patientRepository, GenericRepository<Prescription> prescriptionRepository)
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

        internal Doctor GetDoctor(string firstName, string lastName)
        {
            return doctorRepository.GetByName(firstName, lastName);
        }

        internal Patient GetPatient(string patientFirstName, string patientLastName)
        {
            return patientRepository.GetByName(patientFirstName, patientLastName);
        }

        internal Medication GetMedication(string medicationTradeName, string? dosage)
        {
            return medicationRepository.GetByTradeNameAndDosage(medicationTradeName, dosage);
        }
    }
}
