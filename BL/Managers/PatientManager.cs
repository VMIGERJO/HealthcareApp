using BL.DTO;
using BL.Managers.Interfaces;
using EFDal.Repositories.Interfaces;
using HealthCareAppWPF.DTO;
using EFDal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BL.Managers
{
    public class PatientManager : GenericManager<Patient>, IPatientManager
    {
        internal readonly IPatientRepository _patientRepository;
        public PatientManager(IPatientRepository patientRepository) : base(patientRepository)
        {
            this._patientRepository = patientRepository;
        } 

        public List<PatientBasicDTO> PatientSearch(PatientSearchValuesDTO patientQuery)
        {
            List<Expression<Func<Patient, bool>>> searchExpression = new();

            if (patientQuery?.LastName != null)
                searchExpression.Add(p => p.LastName.Contains(patientQuery.LastName));

            if (patientQuery?.FirstName != null)
                searchExpression.Add(p => p.FirstName.Contains(patientQuery.FirstName));

            var searchResults = _repository.Search(searchExpression, p => p.LastName);

            var result = searchResults.Select(pt => new PatientBasicDTO()
            {
                Name = $"{pt.LastName} {pt.FirstName}",
                Id = pt.Id,
                Age = $"{pt.Age}"
            }).ToList();

            return result;
        }
    }
}
