using BL.DTO;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BL.Managers.Interfaces
{
    public interface IGenericManager<TEntity> where TEntity : BaseEntity
    {
        void Delete(int id);
        Task<TEntity> GetByIdAsync(int id, params Expression<Func<TEntity, object>>[] includes);
        bool Add(TEntity entity);
        void Update(TEntity entity);
    }
}
