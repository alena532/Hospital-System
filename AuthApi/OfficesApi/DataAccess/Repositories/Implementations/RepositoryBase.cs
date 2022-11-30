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
    public void Create(T entity)
    {
        _repositoryContext.Set<T>().Add(entity);
    }

    public async Task SaveChangesAsync()
    {
        await _repositoryContext.SaveChangesAsync();
    }

    public void Delete(T entity)
    {
        _repositoryContext.Set<T>().Remove(entity);
    }
    
    public void Update(T entity)
    {
        _repositoryContext.Set<T>().Update(entity);
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


}