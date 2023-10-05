using Microsoft.AspNetCore.Mvc;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Application.UnitOfWork;
using Domain.Interface;
using iText.Layout.Properties;
using iText.Kernel.Geom;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.IO.Font;
namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : Controller
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    public IActionResult GenerarProductoPdf()
    {
          // Crear un MemoryStream para almacenar el PDF
        MemoryStream ms = new MemoryStream();

        // Crear un nuevo documento PDF
        PdfWriter pdfWriter = new PdfWriter(ms);
        PdfDocument pdfDoc = new PdfDocument(pdfWriter);
        Document doc = new Document(pdfDoc, PageSize.LETTER);
        doc.SetMargins(75,35,70,35);

        //Agregar texto
        PdfFont FontPath =  PdfFontFactory.CreateFont ("arial");
        doc.Add(new Paragraph("Hello Niggers i like pennne")
        .SetFontColor(ColorConstants.BLACK)
        .SetFont(FontPath));
        
        //Agregar Tabla
        Table table = new Table(6);
        table.SetHorizontalAlignment(HorizontalAlignment.CENTER);

        for (int i = 0; i < 1; i++)
        {   
            Cell cell1 = new Cell().Add(new Paragraph("Elemento 1 "));
            Cell cell2 = new Cell().Add(new Paragraph("elemento 2"));
            Cell cell3 = new Cell().Add(new Paragraph("Elemento 4"));
            
            table.AddCell(cell1);
            table.AddCell(cell2);
            table.AddCell(cell3);

            
        }
        // Agregar elemento

        List listanueva = new List()
                .Add(new ListItem("elemnto 1"))
                .Add(new ListItem("Elemento 2"));
        
        doc.Add(listanueva);
        doc.Add(table);
        doc.Close();
        byte[] bytesStream  = ms.ToArray();
        ms = new MemoryStream();
        ms.Write(bytesStream, 0, bytesStream.Length);
        ms.Position = 0;
        return new FileStreamResult(ms, "application/pdf");
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}
