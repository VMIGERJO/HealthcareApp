using AutoMapper;
using BL.DTO;
using BL.MappingProfiles;
using DAL.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestProject.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestProject.MappingTests
{

    [TestClass]
    public class PrescriptionMappingProfileTests
    {
        private IMapper _mapper;
        private EntityFactory _entityFactory;
        private DTOFactory _dtoFactory;

        [TestInitialize]
        public void TestInitialize()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<PrescriptionMappingProfile>());
            config.AssertConfigurationIsValid();
            _mapper = config.CreateMapper();
            _entityFactory = new EntityFactory();
            _dtoFactory = new DTOFactory();
        }

        [TestMethod]
        public void ShouldMapPrescriptionToPrescriptionDTO()
        {
            // Arrange
            Prescription prescription = _entityFactory.CreatePopulatedPrescription(1);

            // Act
            var prescriptionDto = _mapper.Map<PrescriptionDTO>(prescription);

            // Assert
            Assert.IsNotNull(prescriptionDto);
            Assert.AreEqual(prescription.Id, prescriptionDto.Id);
            Assert.AreEqual(prescription.PatientId, prescriptionDto.PatientId);
            Assert.AreEqual(prescription.DoctorId, prescriptionDto.DoctorId);
            Assert.AreEqual(prescription.PrescriptionDate, prescriptionDto.PrescriptionDate);
            Assert.IsNotNull(prescriptionDto.Medications);
            Assert.AreEqual(prescription.Medications.Count, prescriptionDto.Medications.Count);
            // Add more specific assertions for medication properties if needed
        }

        [TestMethod]
        public void ShouldMapPrescriptionDTOToPrescription()
        {
            // Arrange
            var prescriptionDto = _dtoFactory.CreatePopulatedPrescriptionDTO(1);

            // Act
            var prescription = _mapper.Map<Prescription>(prescriptionDto);

            // Assert
            Assert.IsNotNull(prescription);
            Assert.AreEqual(prescriptionDto.Id, prescription.Id);
            Assert.AreEqual(prescriptionDto.PatientId, prescription.PatientId);
            Assert.AreEqual(prescriptionDto.DoctorId, prescription.DoctorId);
            Assert.AreEqual(prescriptionDto.PrescriptionDate, prescription.PrescriptionDate);
            Assert.IsNotNull(prescription.Medications);
            Assert.AreEqual(prescriptionDto.Medications.Count, prescription.Medications.Count);
            foreach (var medication in prescription.Medications)
            {
                var correspondingMedicationDto = prescriptionDto.Medications
                    .FirstOrDefault(m => m.Name == medication.Name);

                Assert.IsNotNull(correspondingMedicationDto);
                Assert.AreEqual(correspondingMedicationDto.ActiveSubstance, medication.ActiveSubstance);
                Assert.AreEqual(correspondingMedicationDto.Name, medication.Name);
                Assert.AreEqual(correspondingMedicationDto.Dosage, medication.Dosage);
                Assert.AreEqual(correspondingMedicationDto.Manufacturer, medication.Manufacturer); // Add this assertion
            }
        }

        [TestMethod]
        public void ShouldMapPrescriptionToPrescriptionViewDTO()
        {
            // Arrange
            Prescription prescription = _entityFactory.CreatePopulatedPrescription(1);

            // Act
            var prescriptionViewDto = _mapper.Map<PrescriptionViewDTO>(prescription);

            // Assert
            Assert.IsNotNull(prescriptionViewDto);
            Assert.AreEqual(prescription.Id, prescriptionViewDto.Id);

            // DoctorName assertion
            Assert.AreEqual($"{prescription.Doctor.LastName} {prescription.Doctor.FirstName}", prescriptionViewDto.DoctorName);

            // PatientName assertion
            Assert.AreEqual($"{prescription.Patient.LastName} {prescription.Patient.FirstName}", prescriptionViewDto.PatientName);

            // Date assertion
            Assert.AreEqual(prescription.PrescriptionDate.ToString("dd/MM/yyyy"), prescriptionViewDto.Date);

            // MedicationNames assertion
            Assert.AreEqual(string.Join(", ", prescription.Medications.Select(m => m.Name)), prescriptionViewDto.MedicationNames);
        }

        [TestMethod]
        public void ShouldMapPrescriptionToPrescriptionSearchValuesDTO()
        {
            // Arrange
            Prescription prescription = _entityFactory.CreatePopulatedPrescription(1);

            // Act
            var searchValuesDto = _mapper.Map<PrescriptionSearchValuesDTO>(prescription);

            // Assert
            Assert.IsNotNull(searchValuesDto);
            Assert.AreEqual(prescription.PatientId, searchValuesDto.PatientID);
            Assert.AreEqual(prescription.DoctorId, searchValuesDto.DoctorID);
        }
    }

}