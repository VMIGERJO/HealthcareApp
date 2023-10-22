using Les2.Entities;

namespace EFDal.Repositories.Interfaces
{
    public interface IDoctorRepository : IGenericRepository<Doctor>
    {
        Doctor GetByName(string firstName, string lastName);
    }
}