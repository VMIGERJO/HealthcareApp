using EFDal.Data;
using EFDal.Exceptions;
using EFDal.Repositories.Interfaces;
using HealthcareApp.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    internal readonly DbContext _context;
    internal readonly DbSet<TEntity> _dbSet;

    public GenericRepository(HealthcareDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = _context.Set<TEntity>();
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    //public virtual async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter)
    //{
    //    return await _dbSet.Where(filter).ToListAsync();
    //}

    public virtual async Task<TEntity> GetByIdAsync(object id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual int Insert(TEntity entity)
    {
        entity.CreatedAt = DateTime.Now;
        _dbSet.Update(entity);
        _context.SaveChanges();
        return entity.Id;
    }

    public void Update(TEntity entity)
    {
        entity.UpdatedAt = DateTime.Now;
        _dbSet.Update(entity);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        _dbSet.Where(x => x.Id == id).ExecuteDelete();
    }

    public async virtual Task<List<TEntity>> SearchAsync(List<Expression<Func<TEntity, bool>>> filters, Expression<Func<TEntity, object>> orderExpression, bool orderAsc = true, params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> queryAble = _dbSet.AsQueryable();

        filters ??= new();

        foreach (Expression<Func<TEntity, bool>> filter in filters)
        {
            if (filter == null)
                continue;
            queryAble = queryAble.Where(filter);
        }

        queryAble = orderAsc ? queryAble.OrderBy(orderExpression) : queryAble.OrderByDescending(orderExpression);

        // Include related entities
        queryAble = includes.Aggregate(queryAble, (current, include) => current.Include(include));

        List<TEntity> result = await queryAble.ToListAsync();

        return result;
    }

    public virtual TEntity UniqueSearch(List<Expression<Func<TEntity, bool>>> filters)
    {
        IQueryable<TEntity> queryAble = _dbSet.AsQueryable();

        filters ??= new();

        foreach (Expression<Func<TEntity, bool>> filter in filters)
        {
            if (filter == null)
                continue;
            queryAble = queryAble.Where(filter);
        }

        List<TEntity> result = queryAble.ToList();

        if (queryAble.Count() == 0)
        {
            throw new NoResultsFoundException();

        } else if (queryAble.Count() > 1)
        {
            throw new NonUniqueQueryException();
        } else
        {
            return queryAble.First();
        }

    }

    public List<TEntity> Search(List<Expression<Func<TEntity, bool>>> filters, Expression<Func<TEntity, object>> orderExpression, bool orderAsc = true)
    {
        IQueryable<TEntity> queryAble = _dbSet.AsQueryable();

        filters ??= new();

        foreach (Expression<Func<TEntity, bool>> filter in filters)
        {
            if (filter == null)
                continue;
            queryAble = queryAble.Where(filter);
        }

        queryAble = orderAsc ? queryAble.OrderBy(orderExpression) : queryAble.OrderByDescending(orderExpression);

        List<TEntity> result = queryAble.ToList();

        return result;
    }
}
