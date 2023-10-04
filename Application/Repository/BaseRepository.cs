
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Interface;
using Persistence.Data;
namespace Application.Repository;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    private readonly PDFGeneratorContext _context;
    public BaseRepository(PDFGeneratorContext context){
        _context = context;
    } 
    public void Add(T entity)
    {
        _context.Set<T>().Add(entity);
    }

    public void AddRange(IEnumerable<T> entities)
    {
        _context.Set<T>().AddRange(entities);
    }

    public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
    {
        return _context.Set<T>().Where(expression);
    }

    public virtual async Task<(int totalRegistros, IEnumerable<T> registros)> GetAllAsync(int pageIndex, int pageSize)
    {
        var totalRegistros = await _context.Set<T>().CountAsync();
        var registros = await _context.Set<T>()
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return (totalRegistros, registros);
    }
    public async Task<T> GetById(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public void Remove(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public void RemoveRange(IEnumerable<T> entities)
    {
        _context.Set<T>().RemoveRange(entities);
    }

}
