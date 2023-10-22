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

    public GenericRepository(DbContext context)
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
        _dbSet.Add(entity);
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

}
