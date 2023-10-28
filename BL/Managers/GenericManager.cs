using BL.Managers.Interfaces;
using HealthcareApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Managers
{
    public class GenericManager<TEntity> : IGenericManager<TEntity>
    where TEntity : BaseEntity
    {
        protected readonly IGenericRepository<TEntity> _repository;

        // Constructs a new instance of the Manager class.
        public GenericManager(IGenericRepository<TEntity> repository)
        {
            _repository = repository;
        }

        // Returns all entities.
        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _repository.GetAllAsync();
        }

        // Returns an entity by id.
        public virtual async Task<TEntity> GetById(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        // Adds an entity.
        public virtual int Add(TEntity entity)
        {
            return _repository.Insert(entity);
        }

        // Updates an entity.
        public virtual void Update(TEntity entity)
        {
            _repository.Update(entity);
        }

        // Removes an entity.
        public virtual void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}
