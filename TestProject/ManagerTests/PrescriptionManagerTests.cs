using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BL.DTO;
using TestProject.Utilities;
using BL.Managers;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using BL.Managers.Interfaces;
using System.Reflection;

namespace TestProject.ManagerTests
{
    [TestClass]
    public class PrescriptionManagerTests
    {
        private PrescriptionManager _prescriptionManager;
        private EntityFactory _entityFactory;
        private DTOFactory _DTOFactory;
        private Mock<IPrescriptionRepository> _prescriptionRepositoryMock;
        private Mock<IMapper> _mapperMock;

        [TestInitialize]
        public void TestInitialize()
        {
            _entityFactory = new EntityFactory();
            _DTOFactory = new DTOFactory();
            _prescriptionRepositoryMock = new Mock<IPrescriptionRepository>();
            _mapperMock = new Mock<IMapper>();
            _prescriptionManager = new PrescriptionManager(_mapperMock.Object, _prescriptionRepositoryMock.Object);
            
        }

        [TestMethod]
        public void Add_ShouldCallMapperAndRepositoryWithCorrectArguments()
        {
            // Arrange
            PrescriptionDTO prescriptionDTO = _DTOFactory.CreatePopulatedPrescriptionDTO(1);
            Prescription newPrescription = _entityFactory.CreatePopulatedPrescription(1);

            _mapperMock.Setup(m => m.Map<Prescription>(prescriptionDTO)).Returns(newPrescription);
            _prescriptionRepositoryMock.Setup(repo => repo.Insert(newPrescription)).Returns(1);

            // Act
            bool result = _prescriptionManager.Add(prescriptionDTO);

            // Assert
            Assert.IsTrue(result);
            _mapperMock.Verify(m => m.Map<Prescription>(prescriptionDTO), Times.Once);
            _prescriptionRepositoryMock.Verify(repo => repo.Insert(newPrescription), Times.Once);
        }

        [TestMethod]
        public async Task PrescriptionSearchAsync_ShouldGenerateCorrectSearchExpression()
        {
            // Arrange
            var prescriptionQuery = new PrescriptionSearchValuesDTO
            {
                PatientID = 1,
                DoctorID = 2,
            };

            // Capture the search expression passed to the repository
            List<Expression<Func<Prescription, bool>>> capturedSearchExpression = null;
            _prescriptionRepositoryMock
                .Setup(repo => repo.SearchPrescriptionsIncludingDoctorPatientMedicationAsync(
                    It.IsAny<List<Expression<Func<Prescription, bool>>>>(),
                    It.IsAny<Expression<Func<Prescription, object>>>(),
                    It.IsAny<bool>()
                    ))
                .Callback<List<Expression<Func<Prescription, bool>>>, Expression<Func<Prescription, object>>, bool>(
                (expressions, orderingExpression, flag) =>
                {
                    capturedSearchExpression = expressions;
                }).ReturnsAsync(new List<Prescription>());

            // Mock the Map method of the IMapper interface
            _mapperMock
                .Setup(mapper => mapper.Map<List<PrescriptionViewDTO>>(It.IsAny<List<Prescription>>()))
                .Returns(new List<PrescriptionViewDTO>()); // Return empty list for simplicity

            // Act
            var result = await _prescriptionManager.PrescriptionSearchAsync(prescriptionQuery);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(capturedSearchExpression);

            // There are two filters in this example
            Assert.AreEqual(2, capturedSearchExpression.Count);

            // Extract the captured expressions and assert their correctness
            var patientFilterExpression = capturedSearchExpression
                .Any(e => e.Compile().Invoke(new Prescription { PatientId = (int)prescriptionQuery.PatientID}));

            var doctorFilterExpression = capturedSearchExpression
                .Any(e => e.Compile().Invoke(new Prescription { DoctorId = (int)prescriptionQuery.DoctorID}));

            Assert.IsTrue(patientFilterExpression);
            Assert.IsTrue(doctorFilterExpression);

            // Verify that the Map method of the IMapper interface is called with the correct arguments
            _mapperMock.Verify(mapper => mapper.Map<List<PrescriptionViewDTO>>(It.IsAny<List<Prescription>>()), Times.Once);
        }


        [TestMethod]
        public async Task GetPrescriptionByIdIncludingMedicationsAsync_ShouldCallRepositoryWithCorrectArguments()
        {
            // Arrange
            var patientId = 1;
            var prescription = _entityFactory.CreatePopulatedPrescription(1);
            var prescriptionDTO = _DTOFactory.CreatePopulatedPrescriptionDTO(1);

            _prescriptionRepositoryMock
                .Setup(repo => repo.GetByIdAsync(
                    patientId,
                    It.Is<Expression<Func<Prescription, object>>>(expr =>
                        CheckExpressionIncludesMedications(expr)
                    )))
                .ReturnsAsync(prescription);
            _mapperMock.Setup(m => m.Map<PrescriptionDTO>(prescription)).Returns(prescriptionDTO);

            // Act
            var result = await _prescriptionManager.GetPrescriptionByIdIncludingMedicationsAsync(patientId);

            // Assert
            Assert.IsNotNull(result);

            _prescriptionRepositoryMock
                .Verify(repo => repo.GetByIdAsync(
                    patientId,
                    It.Is<Expression<Func<Prescription, object>>>(expr =>
                        CheckExpressionIncludesMedications(expr)
                    )),
                    Times.Once);

            _mapperMock.Verify(m => m.Map<PrescriptionDTO>(prescription), Times.Once);
        }

        // Helper method to check if the expression includes Medications
        private bool CheckExpressionIncludesMedications(Expression<Func<Prescription, object>> expr)
        {
            if (expr.Body is MemberExpression memberExpr)
            {
                // Check if the expression represents the Medications property
                return memberExpr.Member.Name == "Medications";
            }
            return false;
        }

    }
}

