using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RepositoryBase.Interfaces;

namespace RepositoryBase.Implementations;

public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    protected DbContext _repositoryContext;
    
    public RepositoryBase(DbContext repositoryContext)
    {
        _repositoryContext = repositoryContext;
    }
    
    public virtual async Task CreateAsync(T entity)
    {
        await _repositoryContext.Set<T>().AddAsync(entity);
        await SaveChangesAsync();
    }
    
    public virtual async Task DeleteAsync(T entity)
    {
        _repositoryContext.Set<T>().Remove(entity);
        await SaveChangesAsync();
    }
    
    public virtual async Task UpdateAsync(T entity)
    {
        _repositoryContext.Set<T>().Update(entity);
        await SaveChangesAsync();
    }

    public virtual IQueryable<T> FindAll(bool trackChanges)
    {
        return (!trackChanges) ?
            _repositoryContext.Set<T>().AsNoTracking() :
            _repositoryContext.Set<T>();
    }

    public virtual IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression,
        bool trackChanges)
    {
        return (!trackChanges) ?
            _repositoryContext.Set<T>().AsNoTracking().Where(expression) :
            _repositoryContext.Set<T>().Where(expression);
    }
    
    public async Task SaveChangesAsync()
    {
        try
        {
            await _repositoryContext.SaveChangesAsync();
        }
        catch (ValidationException e)
        {
            throw new ValidationException(e.Message, e.InnerException);
        }
    }
    


}