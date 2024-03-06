using Business.Entity;

namespace Business.Interface;
public interface IGenericRepository<T> where T : BaseEntity
{
    Task<IEnumerable<T>> GetAll();
    Task<T> GetById(int id);
    void Create(T entity);
}