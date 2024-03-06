

using System.Linq.Expressions;
using Business.Entity;
using Business.Interface;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository;
public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    protected APIContext _context;
    public GenericRepository(APIContext context){
        _context = context;
    }
    public virtual void Create(T entity)
    {
        _context.Set<T>().Add(entity);
    }

    public virtual IEnumerable<T> Find(Expression<Func<T, bool>> expression)
    {
        return _context.Set<T>().Where(expression);
    }

    public virtual async Task<IEnumerable<T>> GetAll()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public virtual async Task<T> GetById(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }
}