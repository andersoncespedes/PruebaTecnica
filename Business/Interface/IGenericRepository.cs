using System.Linq.Expressions;
using Business.Entity;

namespace Business.Interface;
public interface IGenericRepository<T> where T : BaseEntity
{
    Task<IEnumerable<T>> GetAll();
    Task<T> GetById(int id);
    IEnumerable<T> Find(Expression<Func<T, bool>> expression);
    void Create(T entity);
}