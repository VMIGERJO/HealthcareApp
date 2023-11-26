using EFDal.Entities;
using System.Linq.Expressions;

namespace EFDal.Repositories.Interfaces
{
    public interface IPrescriptionRepository : IGenericRepository<Prescription>
    {
        Task<List<Prescription>> SearchPrescriptionsIncludingDoctorPatientMedicationAsync(List<Expression<Func<Prescription, bool>>> searchExpression, Expression<Func<Prescription, object>> orderExpression, bool orderAsc);
    }
}