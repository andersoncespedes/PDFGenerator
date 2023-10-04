
using Domain.Entities;
using Domain.Interface;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using Persistence.Data;

namespace Application.Repository;

public class ProductoRepository : BaseRepository<Producto>, IProducto
{
    public ProductoRepository(PDFGeneratorContext context) : base(context)
    {
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
