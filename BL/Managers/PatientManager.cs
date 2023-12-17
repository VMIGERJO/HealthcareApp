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
using Azure;

namespace BL.Managers
{
    public class PatientManager : GenericManager<Patient>, IPatientManager
    {
        internal readonly IPatientRepository _patientRepository;
        public PatientManager(IMapper mapper, IPatientRepository patientRepository) : base(mapper, patientRepository)
        {
            this._patientRepository = patientRepository;
        }


        public List<string> ValidatePatient(PatientDTO patientDTO)
        {
            List<string> validationErrors = new List<string>();

            //todo eric: validatie zonder ex -> done

            if (patientDTO == null)
            {
                validationErrors.Add("Patient object cannot be null.");
            }
            else
            {
                if (string.IsNullOrWhiteSpace(patientDTO.FirstName))
                {
                    validationErrors.Add("First name must be filled in.");
                }

                if (string.IsNullOrWhiteSpace(patientDTO.LastName))
                {
                    validationErrors.Add("Last name must be filled in.");
                }

                if (patientDTO.Age <= 0 || patientDTO.Age >= 120)
                {
                    validationErrors.Add("Age must be greater than 0 and less than 120.");
                }

                // Validate address
                validationErrors.AddRange(ValidateAddress(patientDTO.Address));
            }

            return validationErrors;
        }

        private List<string> ValidateAddress(AddressDTO addressDTO)
        {
            List<string> validationErrors = new List<string>();

            if (addressDTO == null)
            {
                validationErrors.Add("Address object cannot be null.");
            }
            else
            {
                // Validate each field
                if (string.IsNullOrWhiteSpace(addressDTO.Street))
                {
                    validationErrors.Add("Street must be filled in.");
                }

                if (string.IsNullOrWhiteSpace(addressDTO.HouseNumber))
                {
                    validationErrors.Add("House number must be filled in.");
                }

                if (string.IsNullOrWhiteSpace(addressDTO.City))
                {
                    validationErrors.Add("City must be filled in.");
                }

                if (string.IsNullOrWhiteSpace(addressDTO.PostalCode))
                {
                    validationErrors.Add("Postal code must be filled in.");
                }

                if (string.IsNullOrWhiteSpace(addressDTO.Country))
                {
                    validationErrors.Add("Country must be filled in.");
                }

                // Appartment can be empty, so we don't check it for null or whitespace

                // Convert empty Appartment to null
                addressDTO.Appartment = string.IsNullOrWhiteSpace(addressDTO.Appartment) ? null : addressDTO.Appartment;
            }

            return validationErrors;
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
            List<String> validationErrors = ValidatePatient(patientDTO);
            Patient updatedPatient = Mapper.Map<Patient>(patientDTO);

            if (validationErrors.Count == 0)
            {
                // Update the patient if there are no validation errors
                base.Update(updatedPatient);
            }
            else
            {
                // Throw an exception if validation fails.
                // I don't use this exception to steer the normal program logic, as I also validate in the frontend
                // So this exception should never occur, unless the normal frontend validation is somehow bypassed.
                throw new ArgumentException("Patient did not pass validation");
            }
            
            
        }

        public bool Add(PatientDTO patientDTO)
        {
            List<String> validationErrors = ValidatePatient(patientDTO);
            Patient newPatient = Mapper.Map<Patient>(patientDTO);

            if (validationErrors.Count == 0)
            {
                // Add the patient if there are no validation errors
                return base.Add(newPatient);
            }
            else
            {
                // Throw an exception if validation fails.
                // I don't use this exception to steer the normal program logic, as I also validate in the frontend
                // So this exception should never occur, unless the normal frontend validation is somehow bypassed.
                throw new ArgumentException("Patient did not pass validation:\n" + string.Join("\n", validationErrors));
            }
            
        }

        public async Task<PatientDTO> GetByIdAsync(int patientId)
        {
            Patient patient = await base.GetByIdAsync(patientId);
            return Mapper.Map<PatientDTO>(patient);
        }
    }
}
