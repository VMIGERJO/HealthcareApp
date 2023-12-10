using DAL.Entities;
using BL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Utilities
{
    public class DTOFactory
    {
        public DoctorDTO CreateDoctorDTO()
        {
            return new DoctorDTO
            {
                FirstName = "John",
                LastName = "Doe",
                Specialization = "Cardiologist"
            };
        }

        public void EstablishDoctorPrescriptionDTORelationship(DoctorDTO doctor, PrescriptionDTO prescription)
        {
            // Establish a relationship between Doctor and Prescription
            new List<PrescriptionDTO> { prescription };
        }

        public PatientDTO CreatePatientDTO()
        {
            var addressDTO = CreateAddressDTO();

            return new PatientDTO
            {
                FirstName = "Alice",
                LastName = "Smith",
                Age = 30,
                MedicalHistory = "No significant medical history",
                Address = addressDTO
            };
        }

        public AddressDTO CreateAddressDTO()
        {
            return new AddressDTO
            {
                Street = "123 Main Street",
                HouseNumber = "42",
                Appartment = "Apt 301",
                City = "Cityville",
                PostalCode = "12345",
                Country = "Countryland"
            };
        }

        public void EstablishPatientPrescriptionDTORelationship(PatientDTO patient, PrescriptionDTO prescription)
        {
            // Establish a relationship between Patient and Prescription
            patient.Prescriptions = new List<PrescriptionDTO> { prescription };
        }

        public MedicationDTO CreateMedicationDTO()
        {
            return new MedicationDTO
            {
                ActiveSubstance = "Active Substance",
                Name = "MedicineX",
                Dosage = "10mg",
                Manufacturer = "PharmaCorp"
            };
        }

        public void EstablishPrescriptionMedicationDTORelationship(PrescriptionDTO prescription, MedicationDTO medication)
        {
            // Establish a relationship between Prescription and Medication
            prescription.Medications.Add(medication);
        }

        public PrescriptionDTO CreatePrescriptionDTO(DoctorDTO doctor, PatientDTO patient)
        {
            return new PrescriptionDTO
            {
                DoctorId = doctor.Id,
                PatientId = patient.Id,
                PrescriptionDate = DateTime.Now,
            };
        }

        public PrescriptionDTO CreatePopulatedPrescriptionDTO()
        {
            var doctorDTO = CreateDoctorDTO();
            var patientDTO = CreatePatientDTO();
            var medicationDTO = CreateMedicationDTO();
            var prescriptionDTO = CreatePrescriptionDTO(doctorDTO, patientDTO);
            EstablishPrescriptionMedicationDTORelationship(prescriptionDTO, medicationDTO);
            EstablishPatientPrescriptionDTORelationship(patientDTO, prescriptionDTO);
            EstablishDoctorPrescriptionDTORelationship(doctorDTO, prescriptionDTO);
            return prescriptionDTO;
        }
    }
}
