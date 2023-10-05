
using Domain.Entities;
using Domain.Interface;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Persistence.Data;

namespace Application.Repository;

public class ProductoRepository : BaseRepository<Producto>, IProducto
{
    private readonly PDFGeneratorContext _context;
    public ProductoRepository(PDFGeneratorContext context) : base(context)
    {
        _context = context;
    }
      public override async Task<(int totalRegistros, IEnumerable<Producto> registros)> paginacion (int pageIndex, int pageSize, string _search)
    {
        var totalRegistros = await _context.Set<Producto>().CountAsync();
        var registros = await _context.Set<Producto>()
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .Include(e => e.Marca)
            .ToListAsync();
        return (totalRegistros, registros);
    }
    public void GenerarProductoPdf()
    {

            // Crear un nuevo documento PDF
            Document doc = new Document();
            MemoryStream ms = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(doc, ms);

            doc.Open();

            // Agregar contenido al PDF
            doc.Add(new Paragraph($"Datos de la entidad: "));

            doc.Close();
    
        
      
    }
}
