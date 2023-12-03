using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repositories.Interfaces;
using DAL.Entities;
using System.Linq.Expressions;
using Dapper;

namespace DAL.Repositories.DapperRepositories
{
    public class DapperPrescriptionRepository : DapperGenericRepository<Prescription>, IPrescriptionRepository
    {
        public DapperPrescriptionRepository(DbConnectionFactory dbConnectionFactory) : base(dbConnectionFactory)
        {
        }

        public async Task<List<Prescription>> SearchPrescriptionsIncludingDoctorPatientMedicationAsync(List<Expression<Func<Prescription, bool>>> searchExpression, Expression<Func<Prescription, object>> orderExpression, bool orderAsc)
        {
            string tableName = GetTableName().ToLower();
            StringBuilder query = new StringBuilder($"SELECT * FROM {tableName} ");

            DynamicParameters parameters = new DynamicParameters();

            // Eager load related entities manually as the generic include method can not handle join tables.

            query.Append(" LEFT JOIN doctors ON prescriptions.DoctorId = doctors.Id" +
                " LEFT JOIN patients ON prescriptions.PatientId = patients.Id" +
                " LEFT JOIN MedicationPrescription ON prescriptions.Id = MedicationPrescription.PrescriptionsId" +
                " LEFT JOIN medications ON MedicationPrescription.MedicationsId = medications.Id WHERE 1=1 ");

            ApplyFilters(searchExpression, query, parameters);
            ApplyOrder(orderExpression, orderAsc, query, tableName);


            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                List<Prescription> prescriptions = connection.Query<Prescription, Doctor, Patient, Medication, Prescription>(
                    query.ToString(),
                    (prescription, doctor, patient, medication) =>
                    {
                        prescription.Doctor = doctor;
                        prescription.Patient = patient;
                        prescription.Medications.Add(medication);
                        medication.Prescriptions.Add(prescription);
                        return prescription;
                    },
                    parameters,
                    splitOn: "Id,Id" // Adjust based on the column where the split should occur
                ).ToList();

                return prescriptions;
            }

        }

        public int Insert(Prescription prescription)
        {
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Call the base insert method to insert the Prescription entity
                        int prescriptionId = base.Insert(prescription);

                        // Insert into the many-to-many relationship table
                        foreach (var medication in prescription.Medications)
                        {
                            string joinTableName = "MedicationPrescription"; 
                            string query = $"INSERT INTO {joinTableName} (PrescriptionsId, MedicationsId) VALUES (@PrescriptionId, @MedicationId)";

                            var parameters = new { PrescriptionId = prescriptionId, MedicationId = medication.Id };

                            Dapper.SqlMapper.Execute(connection, query, parameters, transaction); 

                        }

                        // Commit the transaction
                        transaction.Commit();

                        return prescriptionId;
                    }
                    catch (Exception)
                    {
                        // Rollback the transaction in case of an exception
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}
