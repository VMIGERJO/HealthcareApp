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
        public DoctorDTO CreateDoctorDTO(int instanceIndex)
        {
            switch (instanceIndex)
            {
                case 1:
                    return new DoctorDTO
                    {
                        Id = 1,
                        FirstName = "Jake",
                        LastName = "Sully",
                        Specialization = "Emergency Medicine"
                    };
                case 2:
                    return new DoctorDTO
                    {
                        Id = 2,
                        FirstName = "John",
                        LastName = "Doe",
                        Specialization = "Cardiologist"
                    };
                default:
                    throw new ArgumentException("Invalid instance index");
            }
        }


        public PrescriptionSearchValuesDTO CreatePrescriptionSearchValuesDTO(int instanceIndex)
        {
            switch (instanceIndex)
            {
                case 1:
                    return new PrescriptionSearchValuesDTO
                    {
                        PatientID = 1,
                        DoctorID = 1
                    };
                case 2:
                    return new PrescriptionSearchValuesDTO
                    {
                        PatientID = 2,
                        DoctorID = 2
                    };
                default:
                    throw new ArgumentException("Invalid instance index");
            }
        }

        public void EstablishDoctorPrescriptionDTORelationship(DoctorDTO doctor, PrescriptionDTO prescription)
        {
            // Establish a relationship between Doctor and Prescription
            new List<PrescriptionDTO> { prescription };
        }

        public PatientDTO CreatePatientDTO(int instanceIndex)
        {
            switch (instanceIndex)
            {
                case 1:
                    return new PatientDTO
                    {
                        Id = 1,
                        FirstName = "Karen",
                        LastName = "Cooper",
                        Age = 44,
                        MedicalHistory = "Rheuma",
                        Address = CreateAddressDTO(instanceIndex) // You need to provide the addressDTO instance or use CreateAddressDTO method
                    };
                case 2:
                    return new PatientDTO
                    {
                        Id = 2,
                        FirstName = "Alice",
                        LastName = "Smith",
                        Age = 30,
                        MedicalHistory = "No significant medical history",
                        Address = CreateAddressDTO(instanceIndex) // You need to provide the addressDTO instance or use CreateAddressDTO method
                    };
                default:
                    throw new ArgumentException("Invalid instance index");
            }
        }

        public AddressDTO CreateAddressDTO(int instanceIndex)
        {
            switch (instanceIndex)
            {
                case 1:
                    return new AddressDTO
                    {
                        Id = 1,
                        Street = "999 Second Street",
                        HouseNumber = "60",
                        Appartment = "Apt 305",
                        City = "Townville",
                        PostalCode = "98765",
                        Country = "DTOLAND"
                    };
                case 2:
                    return new AddressDTO
                    {
                        Id = 2,
                        Street = "123 Main Street",
                        HouseNumber = "42",
                        Appartment = "Apt 301",
                        City = "Cityville",
                        PostalCode = "12345",
                        Country = "Countryland"
                    };
                default:
                    throw new ArgumentException("Invalid instance index");
            }
        }

        public void EstablishPatientPrescriptionDTORelationship(PatientDTO patient, PrescriptionDTO prescription)
        {
            // Establish a relationship between Patient and Prescription
            patient.Prescriptions = new List<PrescriptionDTO> { prescription };
        }

        public MedicationDTO CreateMedicationDTO(int instanceIndex)
        {

            switch (instanceIndex)
            {
                case 1:
                    return new MedicationDTO
                    {
                        ActiveSubstance = "DTO Substance",
                        Name = "DTOMed",
                        Dosage = "20mg",
                        Manufacturer = "MedCorp"
                    };
                case 2:
                    return new MedicationDTO
                    {
                        ActiveSubstance = "Active Substance",
                        Name = "MedicineX",
                        Dosage = "10mg",
                        Manufacturer = "PharmaCorp"
                    };
                default:
                    throw new ArgumentException("Invalid instance index");
            }
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

        public PrescriptionDTO CreatePopulatedPrescriptionDTO(int instanceIndex)
        {
            var doctorDTO = CreateDoctorDTO(1);
            var patientDTO = CreatePatientDTO(1);
            var medicationDTO = CreateMedicationDTO(1);
            var prescriptionDTO = CreatePrescriptionDTO(doctorDTO, patientDTO);
            EstablishPrescriptionMedicationDTORelationship(prescriptionDTO, medicationDTO);
            EstablishPatientPrescriptionDTORelationship(patientDTO, prescriptionDTO);
            EstablishDoctorPrescriptionDTORelationship(doctorDTO, prescriptionDTO);
            return prescriptionDTO;
        }
    }
}
