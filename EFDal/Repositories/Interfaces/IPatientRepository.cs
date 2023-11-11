using EFDal.Entities;

namespace EFDal.Repositories.Interfaces
{
    public interface IPatientRepository : IGenericRepository<Patient>
    {
        Patient GetByName(string firstName, string lastName);
    }
}