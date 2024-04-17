using ElectraNet.Domain.Commons;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ElectraNet.DataAccess.Repositories;

public class Repository<T> : IRepository<T> where T : Auditable
{
    private readonly AppDbContext context;
    private readonly DbSet<T> set;
    public Repository(AppDbContext context)
    {
        this.context = context;
        this.set = context.Set<T>();
    }

    public async ValueTask<T> InsertAsync(T entity)
    {
        throw new NotImplementedException();
    }
    public async ValueTask<T> UpdateAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<T> DeleteAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<T> DropAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<T> SelectAsync(Expression<Func<T, bool>> expression, string[] includes = null)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<IEnumerable<T>> SelectAsEnumerableAsync(Expression<Func<T, bool>> expression = null,string[] includes = null,bool isTracked = true)
    {
        throw new NotImplementedException();
    }

    public IQueryable<T> SelectAsQueryable(Expression<Func<T, bool>> expression = null,string[] includes = null,bool isTracked = true)
    {
        throw new NotImplementedException();
    }
}