using BL.Managers.Interfaces;
using DAL.Repositories.Interfaces;
using DAL.Entities;
using DAL.Exceptions;
using HealthCareAppWPF.DTO;
using BL.DTO;
using System.Linq.Expressions;
using System.Runtime.ExceptionServices;
using AutoMapper;

namespace BL.Managers
{
    public class DoctorManager : GenericManager<Doctor>, IDoctorManager
    {
        internal readonly IDoctorRepository _doctorRepository;
        public DoctorManager(IMapper mapper, IDoctorRepository doctorRepository) : base(mapper, doctorRepository)
        {
            this._doctorRepository = doctorRepository;
        }

        public void Update(DoctorDTO doctorDTO)
        {
            base.Update(Mapper.Map<Doctor>(doctorDTO));
        }

        public bool Add(DoctorDTO doctorDTO)
        {
            return base.Add(Mapper.Map<Doctor>(doctorDTO));
        }


        public async Task<List<DoctorBasicDTO>> DoctorSearchAsync(DoctorSearchValuesDTO doctorQuery)
        {
            List<Expression<Func<Doctor, bool>>> searchExpression = new();

            if (doctorQuery?.LastName != null)
                searchExpression.Add(p => p.LastName.Contains(doctorQuery.LastName));

            if (doctorQuery?.FirstName != null)
                searchExpression.Add(p => p.FirstName.Contains(doctorQuery.FirstName));

            if (doctorQuery?.Specialization != null)
                searchExpression.Add(p => p.Specialization.Equals(doctorQuery.Specialization));

            var searchResults = await _doctorRepository.SearchAsync(searchExpression, p => p.LastName);

            List<DoctorBasicDTO> result = Mapper.Map<List<DoctorBasicDTO>>(searchResults);

            return result;
        }

        public async Task<List<DoctorBasicDTO>> GetAllDoctorsAsync()
        {
            var searchResults = await _doctorRepository.GetAllAsync();

            List<DoctorBasicDTO> result = Mapper.Map<List<DoctorBasicDTO>>(searchResults);

            return result;
        }

        public async Task<DoctorDTO> UniqueDoctorSearchAsync(DoctorSearchValuesDTO doctorQuery)
        {
            List<Expression<Func<Doctor, bool>>> searchExpression = new();

            if (doctorQuery?.LastName != null)
                searchExpression.Add(p => p.LastName.Contains(doctorQuery.LastName));

            if (doctorQuery?.FirstName != null)
                searchExpression.Add(p => p.FirstName.Contains(doctorQuery.FirstName));
            Doctor doctorSearchResult = await _doctorRepository.SearchUniqueAsync(searchExpression);

            return Mapper.Map<DoctorDTO>(doctorSearchResult);
        }
    }
}
