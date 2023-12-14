using BL.DTO;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace TestProject.Utilities {
public class EntityFactory
{
        

        public void EstablishDoctorPrescriptionRelationship(Doctor doctor, Prescription prescription)
    {
            // Establish a relationship between Doctor and Prescription
           doctor.Prescriptions= new List<Prescription> { prescription };
            prescription.Doctor = doctor;
            prescription.DoctorId = doctor.Id;
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

                case 3:
                    return new Address
                    {
                        Street = "Rainbow street",
                        HouseNumber = "60",
                        Appartment = "Apt 402",
                        City = "Blockville",
                        PostalCode = "87",
                        Country = "Brazil"
                    };
                default:
                    throw new ArgumentException("Invalid instance index");
            }
        }

    public void EstablishPatientPrescriptionRelationship(Patient patient, Prescription prescription)
    {
        // Establish a relationship between Patient and Prescription
        patient.Prescriptions = new List<Prescription> { prescription };
        prescription.Patient = patient;
        prescription.PatientId = patient.Id;
        
    }

        public Doctor CreateDoctor(int instanceIndex)
        {
            switch (instanceIndex)
            {
                case 1:
                    return new Doctor
                    {
                        Id = instanceIndex,
                        FirstName = "Jake",
                        LastName = "Sully",
                        Specialization = "Emergency Medicine"
                    };
                case 2:
                    return new Doctor
                    {
                        Id = instanceIndex,
                        FirstName = "John",
                        LastName = "Doe",
                        Specialization = "Cardiologist"
                    };
                case 3:
                    return new Doctor
                    {
                        Id = instanceIndex,
                        FirstName = "Jane",
                        LastName = "Smith",
                        Specialization = "Pediatrician"
                    };
                default:
                    throw new ArgumentException("Invalid instance index");
            }
        }

        public Patient CreatePatient(int instanceIndex)
        {
            switch (instanceIndex)
            {
                case 1:
                    return new Patient
                    {
                        Id = instanceIndex,
                        FirstName = "Karen",
                        LastName = "Cooper",
                        Age = 44,
                        MedicalHistory = "Rheuma",
                        Address = CreateAddress(instanceIndex)
                    };
                case 2:
                    return new Patient
                    {
                        Id = instanceIndex,
                        FirstName = "Alice",
                        LastName = "Smith",
                        Age = 30,
                        MedicalHistory = "No significant medical history",
                        Address = CreateAddress(instanceIndex)
                    };
                case 3:
                    return new Patient
                    {
                        Id = instanceIndex,
                        FirstName = "Bob",
                        LastName = "Johnson",
                        Age = 35,
                        MedicalHistory = "Allergies",
                        Address = CreateAddress(instanceIndex)
                    };
                default:
                    throw new ArgumentException("Invalid instance index");
            }
        }

        public Medication CreateMedication(int instanceIndex)
        {
            switch (instanceIndex)
            {
                case 1:
                    return new Medication
                    {
                        Id = instanceIndex,
                        ActiveSubstance = "DTO Substance",
                        Name = "DTOMed",
                        Dosage = "20mg",
                        Manufacturer = "MedCorp"
                    };
                case 2:
                    return new Medication
                    {
                        Id = instanceIndex,
                        ActiveSubstance = "Active Substance",
                        Name = "MedicineX",
                        Dosage = "10mg",
                        Manufacturer = "PharmaCorp"
                    };
                case 3:
                    return new Medication
                    {
                        Id = instanceIndex,
                        ActiveSubstance = "New Active Substance",
                        Name = "NewMed",
                        Dosage = "15mg",
                        Manufacturer = "NewCorp"
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

    public Prescription CreatePrescription(Doctor doctor, Patient patient, int instanceIndex)
    {
        return new Prescription
        {
            Id = instanceIndex,
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
            var prescription = CreatePrescription(doctor, patient, instanceIndex);
            EstablishPrescriptionMedicationRelationship(prescription, medication);
            EstablishPatientPrescriptionRelationship(patient, prescription);
            EstablishDoctorPrescriptionRelationship(doctor, prescription);
            return prescription;
        }
  }
}
