using System.Linq.Expressions;
using Domain.Entities;

namespace Domain.Interface;

public interface IBaseRepository<T> where T : BaseEntity
{
    Task<T> GetById(int id);
    Task<(int totalRegistros, IEnumerable<T> registros)> GetAllAsync(int pageIndex, int pageSize);
    IEnumerable<T> Find(Expression<Func<T, bool>> expression);
    void Add(T entity);
    void AddRange(IEnumerable<T> entities);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);

}
