using BL.DTO;
using DAL.Entities;
using System;
using System.Collections.Generic;

namespace TestProject.Utilities {
public class EntityFactory
{
        public Doctor CreateDoctor(int instanceIndex)
        {
            switch (instanceIndex)
            {
                case 1:
                    return new Doctor
                    {
                        FirstName = "Jake",
                        LastName = "Sully",
                        Specialization = "Emergency Medicine"
                    };
                case 2:
                    return new Doctor
                    {
                        FirstName = "John",
                        LastName = "Doe",
                        Specialization = "Cardiologist"
                    };
                default:
                    throw new ArgumentException("Invalid instance index");
            }
        }

        public void EstablishDoctorPrescriptionRelationship(Doctor doctor, Prescription prescription)
    {
            // Establish a relationship between Doctor and Prescription
            new List<Prescription> { prescription };
        }

    public Patient CreatePatient(int instanceIndex)
    {
            switch (instanceIndex)
            {
                case 1:
                    return new Patient
                    {
                        FirstName = "Karen",
                        LastName = "Cooper",
                        Age = 44,
                        MedicalHistory = "Rheuma",
                        Address = CreateAddress(instanceIndex) // You need to provide the addressDTO instance or use CreateAddressDTO method
                    };
                case 2:
                    return new Patient
                    {
                        FirstName = "Alice",
                        LastName = "Smith",
                        Age = 30,
                        MedicalHistory = "No significant medical history",
                        Address = CreateAddress(instanceIndex) // You need to provide the addressDTO instance or use CreateAddressDTO method
                    };
                default:
                    throw new ArgumentException("Invalid instance index");
            }
        }

        public Address CreateAddress(int instanceIndex)
        {
            switch (instanceIndex)
            {
                case 1:
                    return new Address
                    {
                        Street = "999 Second Street",
                        HouseNumber = "60",
                        Appartment = "Apt 305",
                        City = "Townville",
                        PostalCode = "98765",
                        Country = "DTOLAND"
                    };
                case 2:
                    return new Address
                    {
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

    public void EstablishPatientPrescriptionRelationship(Patient patient, Prescription prescription)
    {
        // Establish a relationship between Patient and Prescription
        patient.Prescriptions = new List<Prescription> { prescription };
    }

    public Medication CreateMedication(int instanceIndex)
    {
            switch (instanceIndex)
            {
                case 1:
                    return new Medication
                    {
                        ActiveSubstance = "DTO Substance",
                        Name = "DTOMed",
                        Dosage = "20mg",
                        Manufacturer = "MedCorp"
                    };
                case 2:
                    return new Medication
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

    public void EstablishPrescriptionMedicationRelationship(Prescription prescription, Medication medication)
    {
        // Establish a relationship between Prescription and Medication
        prescription.Medications.Add(medication);
    }

    public Prescription CreatePrescription(Doctor doctor, Patient patient)
    {
        return new Prescription
        {
            Patient = patient,
            Doctor = doctor,
            DoctorId = doctor.Id,
            PatientId = patient.Id,
            PrescriptionDate = DateTime.Now,
        };
    }
    
    public Prescription CreatePopulatedPrescription(int instanceIndex)
        {
            var doctor = CreateDoctor(instanceIndex);
            var patient = CreatePatient(instanceIndex);
            var medication = CreateMedication(instanceIndex);
            var prescription = CreatePrescription(doctor, patient);
            EstablishPrescriptionMedicationRelationship(prescription, medication);
            EstablishPatientPrescriptionRelationship(patient, prescription);
            EstablishDoctorPrescriptionRelationship(doctor, prescription);
            return prescription;
        }
  }
}
