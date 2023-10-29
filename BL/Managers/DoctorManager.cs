using BL.Managers.Interfaces;
using EFDal.Repositories.Interfaces;
using EFDal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCareAppWPF.DTO;
using BL.DTO;
using System.Linq.Expressions;

namespace BL.Managers
{
    public class DoctorManager : GenericManager<Doctor>, IDoctorManager
    {
        internal readonly IDoctorRepository _doctorRepository;
        public DoctorManager(IDoctorRepository doctorRepository) : base(doctorRepository)
        {
            this._doctorRepository = doctorRepository;
        }

        public List<DoctorBasicDTO> DoctorSearch(DoctorSearchValuesDTO doctorQuery)
        {
            List<Expression<Func<Doctor, bool>>> searchExpression = new();

            if (doctorQuery?.LastName != null)
                searchExpression.Add(p => p.LastName.Contains(doctorQuery.LastName));

            if (doctorQuery?.FirstName != null)
                searchExpression.Add(p => p.FirstName.Contains(doctorQuery.FirstName));

            var searchResults = _repository.Search(searchExpression, p => p.LastName);

            var result = searchResults.Select(pt => new DoctorBasicDTO()
            {
                Name = $"{pt.LastName} {pt.FirstName}",
                Id = pt.Id,
                Specialization = $"{pt.Specialization}"
            }).ToList();

            return result;
        }
    }
}
