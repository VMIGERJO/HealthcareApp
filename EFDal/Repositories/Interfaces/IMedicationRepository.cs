using EFDal.Entities;

namespace EFDal.Repositories.Interfaces
{
    public interface IMedicationRepository : IGenericRepository<Medication>
    {
        Medication GetByTradeNameAndDosage(string tradeName, string dosage);
    }
}