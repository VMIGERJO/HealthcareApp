using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BL.DTO;
using DAL.Data;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TestProject.Utilities;

namespace TestProject.UnitTests
{
    [TestClass]
    public class GenericRepositoryTests
    {
        private HealthcareDbContext _dbContext;
        private GenericRepository<Prescription> _repository;

        [TestInitialize]
        public void TestInitialize()
        {
            // Initialize the in-memory database and repository for each test
            var options = new DbContextOptionsBuilder<HealthcareDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _dbContext = new HealthcareDbContext(options);

            // Seed the database with test data
            var entityFactory = new EntityFactory();
            var prescription1 = entityFactory.CreatePopulatedPrescription(1);
            var prescription2 = entityFactory.CreatePopulatedPrescription(2);
            _dbContext.AddRange(prescription1, prescription2);
            _dbContext.SaveChanges();

            _repository = new GenericRepository<Prescription>(_dbContext);
        }

        [TestMethod]
        public async Task GetAllAsync_ShouldReturnAllEntities()
        {
            // Act
            var result = await _repository.GetAllAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count); 

            Assert.IsTrue(result.Any(prescription => prescription.Id == 1));
            Assert.IsTrue(result.Any(prescription => prescription.Id == 2));
        }


        [TestMethod]
        public async Task GetByIdAsync_ShouldReturnEntityById()
        {
            // Arrange
            var entityId = 1; // Change with an existing entity id

            // Act
            var result = await _repository.GetByIdAsync(entityId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(entityId, result.Id);
            Assert.AreEqual("Karen", result.Patient.FirstName); // Adjust based on your actual data
            // Add more assertions based on your entity structure
        }

        [TestMethod]
        public void Insert_ShouldInsertEntity()
        {
            // Arrange
            var entity = new EntityFactory().CreatePopulatedPrescription(3); // Use a new instance for testing

            // Act
            var insertedId = _repository.Insert(entity);

            // Assert
            Assert.AreEqual(entity.Id, insertedId);
            var insertedEntity = _dbContext.Set<Prescription>().FirstOrDefault(e => e.Id == insertedId);
            Assert.IsNotNull(insertedEntity);
            Assert.AreEqual(3, insertedEntity.Id);
        }

        [TestMethod]
        public void Update_ShouldUpdateEntity()
        {
            // Arrange
            var entity = _dbContext.Set<Prescription>().Find(1);
            entity.Doctor.FirstName = "UpdatedName";

            // Act
            _repository.Update(entity);

            // Assert
            var updatedEntity = _dbContext.Set<Prescription>().FirstOrDefault(e => e.Id == entity.Id);
            Assert.IsNotNull(updatedEntity);
            Assert.AreEqual("UpdatedName", updatedEntity.Doctor.FirstName);
        }

        // The method below doesn't work. According to stackoverflow: These new extensions only work on relational providers. And InMemory is not a relational provider
        // Referring to ExecuteDelete().

        //[TestMethod]
        //public void Delete_ShouldDeleteEntity()
        //{
        //    // Arrange
        //    int entityId = 1; 

        //    // Act
        //    _repository.Delete(entityId);

        //    // Assert
        //    var deletedEntity = _dbContext.Set<Prescription>().Find(entityId);
        //    Assert.IsNull(deletedEntity);
        //    // Add more assertions based on your entity structure
        //}

        [TestMethod]
        public async Task SearchAsync_ShouldReturnFilteredEntities()
        {
            // Arrange
            var prescription = new EntityFactory().CreatePopulatedPrescription(1);
            string exampleDoctorFirstName = prescription.Doctor.FirstName;
            var filters = new List<Expression<Func<Prescription, bool>>>
            {
                p => p.Doctor.FirstName == exampleDoctorFirstName
            };

            // Act
            var result = await _repository.SearchAsync(filters, e => e.DoctorId, true, e => e.Doctor);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(exampleDoctorFirstName, result[0].Doctor.FirstName);
        }

        [TestMethod]
        public async Task SearchUniqueAsync_ShouldReturnUniqueEntity()
        {
            // Arrange
            var filters = new List<Expression<Func<Prescription, bool>>>
            {
                // Add your filters based on your entity structure
            };

            // Act & Assert
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => _repository.SearchUniqueAsync(filters));
        }

        [TestCleanup]
        public void TestCleanup()
        {
            // Clean up resources after each test
            _dbContext.Dispose();


        }
    }
}
