using DAL.Data;
using DAL.Exceptions;
using DAL.Repositories.Interfaces;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DAL.ExtensionMethods;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    internal readonly DbContext _context;
    internal readonly DbSet<TEntity> _dbSet;

    public GenericRepository(HealthcareDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = _context.Set<TEntity>();
    }

    public virtual async Task<List<TEntity>> GetAllAsync()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }


    public virtual async Task<TEntity> GetByIdAsync(int id, params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> queryAble = _dbSet.AsQueryable();
        // Include related entities
        queryAble = includes.Aggregate(queryAble, (current, include) => current.Include(include));
        return await queryAble.FirstOrDefaultAsync(e => e.Id == id);

    }

    public virtual int Insert(TEntity entity)
    {
        _dbSet.Add(entity);
        _context.SaveChanges();
        return entity.Id;
    }

    public void Update(TEntity entity)
    {

        // Check if the entity is already tracked by the context
        var existingEntity = _context.Set<TEntity>().Local.FirstOrDefault(e => e.Id == entity.Id);

        if (existingEntity != null)
        {
            // Update the existing tracked entity with the values from the new entity
            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
        }
        else
        {
            // Attach the entity to the context if it's not already tracked
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

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

        // Ensure no - tracking behavior
        queryAble = queryAble.AsNoTracking();

        // Include related entities
        queryAble = includes.Aggregate(queryAble, (current, include) => current.Include(include));

        List<TEntity> result = await queryAble.ToListAsync();

        return result;
    }

    public async virtual Task<TEntity> SearchUniqueAsync(List<Expression<Func<TEntity, bool>>> filters, params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> queryAble = _dbSet.AsQueryable();

        filters ??= new();

        foreach (Expression<Func<TEntity, bool>> filter in filters)
        {
            if (filter == null)
                continue;
            queryAble = queryAble.Where(filter);
        }

        // Ensure no - tracking behavior
        queryAble = queryAble.AsNoTracking();

        // Include related entities
        queryAble = includes.Aggregate(queryAble, (current, include) => current.Include(include));

        //todo eric: .singleordef
        List<TEntity> result = await queryAble.ToListAsync();
        //var result2 = await queryAble.SingleOrDefaultAsync();

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

    
}
