using Application.Repository;
using Domain.Interface;
using Persistence.Data;
using Domain.Interface;
namespace Application.UnitOfWork;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private ProductoRepository _productos;
    private readonly PDFGeneratorContext _context;
    public UnitOfWork(PDFGeneratorContext context){
        _context = context;
    }
    public IProducto Productos{get{
        if(_productos == null){
            _productos = new ProductoRepository(_context);
        }
        return _productos;
    }}

    public void Dispose()
    {
        _context.Dispose();
    }
}
