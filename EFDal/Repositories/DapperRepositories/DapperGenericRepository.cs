using DAL.Data;
using DAL.Entities;
using DAL.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.DapperRepositories
{
    public class DapperGenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        public DapperGenericRepository()
        {
            throw new NotImplementedException();
        }

        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            throw new NotImplementedException();

        }


        public virtual async Task<TEntity> GetByIdAsync(int id, params Expression<Func<TEntity, object>>[] includes)
        {
            throw new NotImplementedException();

        }

        public virtual int Insert(TEntity entity)
        {
            throw new NotImplementedException();

        }

        public void Update(TEntity entity)
        {
            throw new NotImplementedException();

        }

        public void Delete(int id)
        {
            throw new NotImplementedException();

        }

        public async virtual Task<List<TEntity>> SearchAsync(List<Expression<Func<TEntity, bool>>> filters, Expression<Func<TEntity, object>> orderExpression, bool orderAsc = true, params Expression<Func<TEntity, object>>[] includes)
        {
            throw new NotImplementedException();

        }

        public async virtual Task<TEntity> SearchUniqueAsync(List<Expression<Func<TEntity, bool>>> filters, params Expression<Func<TEntity, object>>[] includes)
        {
            throw new NotImplementedException();

        }


    }

}
