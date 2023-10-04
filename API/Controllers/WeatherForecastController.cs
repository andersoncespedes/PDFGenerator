using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Application.UnitOfWork;
using Domain.Interface;
using Microsoft.AspNetCore.Http;
using iText.Layout.Properties;
using iText.Kernel.Geom;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using Microsoft.OpenApi.Expressions;
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
    [HttpGet("PDF")]
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
        doc.Add(new Paragraph("Hello Niggers i like pennne").SetFontColor(ColorConstants.BLACK));

        //Agregar Tabla
        Table table = new Table(3);
        table.SetHorizontalAlignment(HorizontalAlignment.CENTER);

        Cell headerCe1 = new Cell().Add(new Paragraph("Columna 1"));
        Cell headerCe2 = new Cell().Add(new Paragraph("Columna 2"));
        Cell headerCe3 = new Cell().Add(new Paragraph("Columna 3"));
        

        table.AddHeaderCell(headerCe1);
        table.AddHeaderCell(headerCe2);
        table.AddHeaderCell(headerCe3);

        for (int i = 0; i < 4; i++)
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
