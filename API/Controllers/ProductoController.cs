
using Domain.Interface;
using API.Helpers;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using System;
using System.Reflection;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Kernel.Geom;
using iText.Kernel.Colors;
using AutoMapper;
using API.Dtos;
namespace API.Controllers;

public class ProductoController : BaseApiController
{
    private IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public ProductoController(IUnitOfWork unitOfWork, IMapper mapper){
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    [HttpGet("pdf")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<ProductoDto>>> Get11([FromQuery] Params productParams)
    {
        var products = await _unitOfWork.Productos.paginacion(productParams.PageIndex,productParams.PageSize,productParams.Search);
        var lstProductDto = _mapper.Map<List<ProductoDto>>(products.registros);
        MemoryStream ms = new MemoryStream();

        // Crear un nuevo documento PDF
        PdfWriter pdfWriter = new PdfWriter(ms);
        PdfDocument pdfDoc = new PdfDocument(pdfWriter);
        Document doc = new Document(pdfDoc, PageSize.LETTER);
        doc.SetMargins(75,35,70,35);

        //Agregar texto
        doc.Add(new Paragraph("Hello Niggers i like pennne")
        .SetFontColor(ColorConstants.BLACK));
        
        //Agregar Tabla
        Table table = new Table(products.totalRegistros);
        table.SetHorizontalAlignment(HorizontalAlignment.CENTER);
        foreach(var x in lstProductDto){
           
            Type type = x.GetType();
            PropertyInfo[] propiedad = type.GetProperties();
            foreach(var i in propiedad){
                Cell cell1 = new Cell().Add(new Paragraph(i.Name ));
                Cell cell2 = new Cell().Add(new Paragraph(i.GetValue(x).ToString() ));

                table.AddCell(cell1);
                table.AddCell(cell2);

            }
            
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
}
