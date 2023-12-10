using DAL.Entities;
using System;
using System.Collections.Generic;

namespace TestProject.Utilities {
public class EntityFactory
{
    public Doctor CreateDoctor()
    {
        return new Doctor
        {
            FirstName = "John",
            LastName = "Doe",
            Specialization = "Cardiologist"
        };
    }

    public void EstablishDoctorPrescriptionRelationship(Doctor doctor, Prescription prescription)
    {
            // Establish a relationship between Doctor and Prescription
            new List<Prescription> { prescription };
        }

    public Patient CreatePatient()
    {
        var address = CreateAddress();

        return new Patient
        {
            FirstName = "Alice",
            LastName = "Smith",
            Age = 30,
            MedicalHistory = "No significant medical history",
            Address = address
        };
    }

    public Address CreateAddress()
    {
        return new Address
        {
            Street = "123 Main Street",
            HouseNumber = "42",
            Appartment = "Apt 301",
            City = "Cityville",
            PostalCode = "12345",
            Country = "Countryland"
        };
    }

    public void EstablishPatientPrescriptionRelationship(Patient patient, Prescription prescription)
    {
        // Establish a relationship between Patient and Prescription
        patient.Prescriptions = new List<Prescription> { prescription };
    }

    public Medication CreateMedication()
    {
        return new Medication
        {
            ActiveSubstance = "Active Substance",
            Name = "MedicineX",
            Dosage = "10mg",
            Manufacturer = "PharmaCorp"
        };
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
    
    public Prescription CreatePopulatedPrescription()
        {
            var doctor = CreateDoctor();
            var patient = CreatePatient();
            var medication = CreateMedication();
            var prescription = CreatePrescription(doctor, patient);
            EstablishPrescriptionMedicationRelationship(prescription, medication);
            EstablishPatientPrescriptionRelationship(patient, prescription);
            EstablishDoctorPrescriptionRelationship(doctor, prescription);
            return prescription;
        }
  }
}
