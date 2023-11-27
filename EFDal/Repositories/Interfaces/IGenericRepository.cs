﻿using EFDal.Entities;
using System.Linq.Expressions;

public interface IGenericRepository<TEntity> where TEntity : BaseEntity
{
    void Delete(int id);
    Task<List<TEntity>> GetAllAsync();
    Task<TEntity> GetByIdAsync(object id);
    int Insert(TEntity entity);
    void Update(TEntity entity);
    List<TEntity> Search(List<Expression<Func<TEntity, bool>>> filters, Expression<Func<TEntity, object>> orderExpression, bool orderAsc=true);
    Task<List<TEntity>> SearchAsync(List<Expression<Func<TEntity, bool>>> filters, Expression<Func<TEntity, object>> orderExpression, bool orderAsc = true, params Expression<Func<TEntity, object>>[] includes);

    Task<TEntity> SearchUniqueAsync(List<Expression<Func<TEntity, bool>>> filters, params Expression<Func<TEntity, object>>[] includes);

}