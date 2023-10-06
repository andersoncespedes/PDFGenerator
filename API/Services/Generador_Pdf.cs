using System;
using System.Collections.Generic;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System.Reflection;
using iText.Layout.Properties;
using iText.Kernel.Geom;
using iText.Kernel.Colors;
namespace API.Services;

public class Generador_Pdf
{
    public void Document<T>(List<T> lstProductDto, MemoryStream ms ){
        PdfWriter pdfWriter = new PdfWriter(ms);
        PdfDocument pdfDoc = new PdfDocument(pdfWriter);
        Document doc = new Document(pdfDoc, PageSize.LETTER);
        doc.SetMargins(75, 35, 70, 35);
        doc.SetTextAlignment(TextAlignment.CENTER);
        PropertyInfo[] propiedades = lstProductDto[0].GetType().GetProperties();
        doc.Add(new Paragraph("Hola Como Estas"));
        // Agregar Tabla
        Table table = new Table(propiedades.Length);
        table.SetMarginTop(10); // Ajusta el espacio entre la tabla y el contenido superior del documento
        table.SetHorizontalAlignment(HorizontalAlignment.CENTER);

        // Agregar encabezados a la tabla
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
    }
    public MemoryStream Generador<T>(List<T> lstProductDto)
    {
        MemoryStream ms = new MemoryStream();
        // Crear un nuevo documento PDF
        Document(lstProductDto, ms);
        byte[] bytesStream = ms.ToArray();
        ms = new MemoryStream();
        ms.Write(bytesStream, 0, bytesStream.Length);
        ms.Position = 0;
        return ms;
    }
}
