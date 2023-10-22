using HealthcareApp.Entities;
using System.Linq.Expressions;

public interface IGenericRepository<TEntity> where TEntity : BaseEntity
{
    void Delete(int id);
    Task<IEnumerable<TEntity>> GetAllAsync();
    // Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter);
    Task<TEntity> GetByIdAsync(object id);
    int Insert(TEntity entity);
    void Update(TEntity entity);
}