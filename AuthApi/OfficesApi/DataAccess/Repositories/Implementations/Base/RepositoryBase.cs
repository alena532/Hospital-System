
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using OfficesApi.DataAccess.Repositories.Base;

namespace OfficesApi.DataAccess.Repositories.Implementations;

public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    protected AppDbContext _repositoryContext;
    
    public RepositoryBase(AppDbContext repositoryContext)
    {
        _repositoryContext = repositoryContext;
    }
    
    public async Task CreateAsync(T entity)
    {
        await _repositoryContext.Set<T>().AddAsync(entity);
        await SaveChangesAsync();
    }
    
    public async Task DeleteAsync(T entity)
    {
        _repositoryContext.Set<T>().Remove(entity);
        await SaveChangesAsync();
    }
    
    public async Task UpdateAsync(T entity)
    {
        _repositoryContext.Set<T>().Update(entity);
        await SaveChangesAsync();
    }

    public IQueryable<T> FindAll(bool trackChanges)
    {
        return (!trackChanges) ?
            _repositoryContext.Set<T>().AsNoTracking() :
            _repositoryContext.Set<T>();
    }

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression,
        bool trackChanges)
    {
        return (!trackChanges) ?
            _repositoryContext.Set<T>().AsNoTracking().Where(expression) :
            _repositoryContext.Set<T>().Where(expression);
    }
    
    public Task SaveChangesAsync()
    {
        try
        {
            return  _repositoryContext.SaveChangesAsync();
        }
        catch (ValidationException e)
        {
            throw new ValidationException(e.Message, e.InnerException);
        }
    }


}