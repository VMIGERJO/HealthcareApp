using EFDal.Entities;
using System.Linq.Expressions;

namespace EFDal.Repositories.Interfaces
{
    public interface IPatientRepository : IGenericRepository<Patient>
    {
        Task<Patient> SearchPatientWithAddressAsync(List<Expression<Func<Patient, bool>>> searchExpression);
    }
}