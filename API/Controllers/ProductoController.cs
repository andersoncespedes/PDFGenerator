using Domain.Interface;
using API.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Kernel.Geom;
using iText.Kernel.Colors;
using AutoMapper;
using API.Dtos;
using iText.Layout.Borders;

namespace API.Controllers
{
    public class ProductoController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductoController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("pdf")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<ProductoDto>>> Get11([FromQuery] Params productParams)
        {
            var products = await _unitOfWork.Productos.paginacion(productParams.PageIndex, productParams.PageSize, productParams.Search);
            var lstProductDto = _mapper.Map<List<ProductoDto>>(products.registros);
            MemoryStream ms = new MemoryStream();

            // Crear un nuevo documento PDF
            PdfWriter pdfWriter = new PdfWriter(ms);
            PdfDocument pdfDoc = new PdfDocument(pdfWriter);
            Document doc = new Document(pdfDoc, PageSize.LETTER);
            doc.SetMargins(75, 35, 70, 35);
            doc.SetTextAlignment(TextAlignment.CENTER);

            // Agregar Tabla
            Table table = new Table(products.totalRegistros);
            table.SetMarginTop(10); // Ajusta el espacio entre la tabla y el contenido superior del documento
            table.SetHorizontalAlignment(HorizontalAlignment.CENTER);

            // Agregar encabezados a la tabla
            PropertyInfo[] propiedades = lstProductDto[0].GetType().GetProperties();
            foreach (var propiedad in propiedades)
            {
                Cell headerCell = new Cell().Add(new Paragraph(propiedad.Name)
                    .SetBold() //texto en negrita
                    .SetFontSize(10) //tamaño de letra
                    .SetBackgroundColor(new DeviceRgb(255, 215, 0)) //color de fondo
                    .SetPadding(10)); // ajustar el relleno
                headerCell.SetTextAlignment(TextAlignment.CENTER);

                table.AddCell(headerCell);
            }

            // Agregar datos a la tabla
            foreach (var x in lstProductDto)
            {
                foreach (var propiedad in propiedades)
                {
                    Cell dataCell = new Cell().Add(new Paragraph(propiedad.GetValue(x).ToString())
                        .SetFontSize(8) // tamaño de fuente
                        .SetPadding(5)); // el relleno
                    dataCell.SetTextAlignment(TextAlignment.CENTER);

                    table.AddCell(dataCell);
                }
            }

            // Agregar la tabla al documento
            doc.Add(table);
            doc.Close();

            byte[] bytesStream = ms.ToArray();
            ms = new MemoryStream();
            ms.Write(bytesStream, 0, bytesStream.Length);
            ms.Position = 0;

            return new FileStreamResult(ms, "application/pdf");
        }
    }
}
