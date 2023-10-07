
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System.Reflection;
using iText.Layout.Properties;
using iText.Kernel.Geom;
using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.IO.Image;
using iText.Layout.Borders;
using Microsoft.VisualBasic;

namespace API.Services;

public class Generador_Pdf
{
    public void Header(Document doc, PdfDocument pdfDocument)
    {
        PdfFont font = PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN);
        Paragraph paragraph = new Paragraph();
        Image image = new Image(ImageDataFactory.Create("C:/Users/APOLT01/Desktop/PDFGenerator/API/Image/campuslands_logo.jpg"));
        DateTime date = DateTime.Now;
        image.SetFixedPosition(pdfDocument.GetDefaultPageSize().GetWidth() - 600, pdfDocument.GetDefaultPageSize().GetHeight() - 160);
        image.ScaleToFit(100, 100);
        doc.SetTextAlignment(TextAlignment.RIGHT);
        doc.SetMargins(55, 35, 70, 35);
        doc.SetTextAlignment(TextAlignment.RIGHT);
        doc.SetFontSize(10);
        doc.Add(new Paragraph("Reporte CampusLands Medicamentos")
            .SetMargin(1)
            .SetFontSize(10)
            .SetFontSize(20));
        doc.Add(new Paragraph("Anderson Cepedez")
            .SetMargin(1));
        doc.Add(new Paragraph("Diego Muñoz").SetMargin(1));
        doc.Add(new Paragraph(date.ToString()).SetMargin(1));

        doc.SetTextAlignment(TextAlignment.CENTER);
        doc.Add(new Paragraph("Reporte de productos").SetFontSize(30).SetFont(font).SetUnderline());
        doc.Add(image);
    }
    public void Document<T>(List<T> lstProductDto, MemoryStream ms)
    {   ;
        PdfWriter pdfWriter = new (ms);
        PdfDocument pdfDoc = new (pdfWriter);
        Document doc = new (pdfDoc, PageSize.LETTER);

        doc.SetMargins(75, 35, 70, 35);
        doc.SetBorder(new SolidBorder(2f));
        Header(doc, pdfDoc);
        PropertyInfo[] propiedades = lstProductDto[0].GetType().GetProperties();
        // Agregar Tabla
        doc.SetTextAlignment(TextAlignment.CENTER);
        Table table = new Table(propiedades.Length);
        table.SetMarginTop(10); // Ajusta el espacio entre la tabla y el contenido superior del documento
        table.SetHorizontalAlignment(HorizontalAlignment.CENTER);
        // Agregar encabezados a la tabla
        foreach (var propiedad in propiedades)
        {
            Cell headerCell = new Cell()
            .SetPadding(0)
            .Add(new Paragraph(propiedad.Name)
                .SetBold() //texto en negrita
                .SetFontSize(12) //tamaño de letra
                .SetBackgroundColor(new DeviceRgb(210, 210, 210)
                ).SetPadding(10) //color de fondo
               );
            headerCell.SetTextAlignment(TextAlignment.CENTER);

            table.AddCell(headerCell);
        }

        // Agregar datos a la tabla
        foreach (var x in lstProductDto)
        {
            foreach (var propiedad in propiedades)
            {
                Cell dataCell = new Cell().Add(new Paragraph(propiedad.GetValue(x).ToString())
                    .SetFontSize(12) // tamaño de fuente
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
