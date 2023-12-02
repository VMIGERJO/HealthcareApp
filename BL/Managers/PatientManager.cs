using BL.DTO;
using BL.Managers.Interfaces;
using DAL.Repositories.Interfaces;
using HealthCareAppWPF.DTO;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace BL.Managers
{
    public class PatientManager : GenericManager<Patient>, IPatientManager
    {
        internal readonly IPatientRepository _patientRepository;
        public PatientManager(IMapper mapper, IPatientRepository patientRepository) : base(mapper, patientRepository)
        {
            this._patientRepository = patientRepository;
        }


        private void ValidatePatient(Patient patient)
        {
            if (patient == null)
            {
                throw new ArgumentNullException(nameof(patient), "Patient object cannot be null.");
            }

            // Validate name and age
            if (string.IsNullOrWhiteSpace(patient.FirstName))
            {
                throw new ArgumentException("First name must be filled in.", nameof(patient.FirstName));
            }

            if (string.IsNullOrWhiteSpace(patient.LastName))
            {
                throw new ArgumentException("Last name must be filled in.", nameof(patient.LastName));
            }

            if (patient.Age <= 0 || patient.Age >= 120)
            {
                throw new ArgumentOutOfRangeException(nameof(patient.Age), "Age must be greater than 0 and less than 120.");
            }

            // Validate address
            ValidateAddress(patient.Address);
        }

        private void ValidateAddress(Address address)
        {
            if (address == null)
            {
                throw new ArgumentNullException(nameof(address), "Address object cannot be null.");
            }

            // Validate each field
            if (string.IsNullOrWhiteSpace(address.Street))
            {
                throw new ArgumentException("Street must be filled in.", nameof(address.Street));
            }

            if (string.IsNullOrWhiteSpace(address.HouseNumber))
            {
                throw new ArgumentException("House number must be filled in.", nameof(address.HouseNumber));
            }

            if (string.IsNullOrWhiteSpace(address.City))
            {
                throw new ArgumentException("City must be filled in.", nameof(address.City));
            }

            if (string.IsNullOrWhiteSpace(address.PostalCode))
            {
                throw new ArgumentException("Postal code must be filled in.", nameof(address.PostalCode));
            }

            if (string.IsNullOrWhiteSpace(address.Country))
            {
                throw new ArgumentException("Country must be filled in.", nameof(address.Country));
            }

            // Appartment can be empty, so we don't check it for null or whitespace

            // Convert empty Appartment to null
            address.Appartment = string.IsNullOrWhiteSpace(address.Appartment) ? null : address.Appartment;
        }

        public async Task<List<PatientBasicDTO>> SearchPatientsAsync(PatientSearchValuesDTO patientQuery)
        {
            List<Expression<Func<Patient, bool>>> searchExpression = new();

            if (patientQuery?.LastName != null)
                searchExpression.Add(p => p.LastName.Contains(patientQuery.LastName));

            if (patientQuery?.FirstName != null)
                searchExpression.Add(p => p.FirstName.Contains(patientQuery.FirstName));

            List<Patient> searchResults = await _patientRepository.SearchAsync(searchExpression, p => p.LastName);

            List<PatientBasicDTO> result = searchResults.Select(pt => new PatientBasicDTO()
            {
                Name = $"{pt.LastName} {pt.FirstName}",
                Id = pt.Id,
                Age = $"{pt.Age}"
            }).ToList();

            return result;
        }

        public async Task<PatientDTO> SearchPatientWithAdressAsync(PatientSearchValuesDTO patientQuery)
        {
            List<Expression<Func<Patient, bool>>> searchExpression = new();

            if (patientQuery?.LastName != null)
                searchExpression.Add(p => p.LastName.Contains(patientQuery.LastName));

            if (patientQuery?.FirstName != null)
                searchExpression.Add(p => p.FirstName.Contains(patientQuery.FirstName));

            Patient patient = await _patientRepository.SearchPatientWithAddressAsync(searchExpression);

            return Mapper.Map<PatientDTO>(patient);
        }

        public async Task<PatientDTO> GetPatientByIdIncludingAddressAsync(int patientId)
        {
            Patient patient =  await _patientRepository.GetPatientByIdIncludingAddressAsync(patientId);
            return Mapper.Map<PatientDTO>(patient);
        }


        public void Update(PatientDTO patientDTO)
        {
            Patient updatedPatient = Mapper.Map<Patient>(patientDTO);
            ValidatePatient(updatedPatient);
            base.Update(updatedPatient);
        }

        public bool Add(PatientDTO patientDTO)
        {
            Patient newPatient = Mapper.Map<Patient>(patientDTO);
            ValidatePatient(newPatient);
            return base.Add(newPatient);
        }

        public async Task<PatientDTO> GetByIdAsync(int patientId)
        {
            Patient patient = await base.GetByIdAsync(patientId);
            return Mapper.Map<PatientDTO>(patient);
        }
    }
}
