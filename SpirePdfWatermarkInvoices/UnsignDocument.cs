using Spire.Pdf;
using Spire.Pdf.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpirePdfWatermarkInvoices
{
    internal class UnsignDocument
    {
        public PdfDocument Unsign(PdfDocument document)
        {
            PdfDocument unsignedPdf = new PdfDocument();

            foreach(PdfPageBase page in document.Pages)
            {
                SizeF size = page.Size;
                PdfTemplate template = page.CreateTemplate();
                var addedPage = unsignedPdf.Pages.Add(size, new PdfMargins(0, 0));
                addedPage.Canvas.DrawTemplate(template, new PointF(0, 0));
            }
            return unsignedPdf; 
        }
    }
}
