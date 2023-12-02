using AutoMapper;
using BL.Managers.Interfaces;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BL.Managers
{
    public class GenericManager<TEntity> : IGenericManager<TEntity>
    where TEntity : BaseEntity
    {
        private readonly IGenericRepository<TEntity> _repository;
        protected IMapper Mapper { get; }

        // Constructs a new instance of the Manager class.
        public GenericManager(IMapper mapper, IGenericRepository<TEntity> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        // Returns all entities.
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        // Returns an entity by id.
        public virtual async Task<TEntity> GetByIdAsync(int id, params Expression<Func<TEntity, object>>[] includes)
        {
            return await _repository.GetByIdAsync(id, includes);
        }

        // Adds an entity.
        public virtual bool Add(TEntity entity)
        {
            try
            {
                int result = _repository.Insert(entity);
                return result > 0;
            }
            catch (Exception ex)
            {
                // Log the exception to the console
                Console.WriteLine($"Exception while adding entity of type {typeof(TEntity)}: {ex.Message}");
                return false;
            }
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
