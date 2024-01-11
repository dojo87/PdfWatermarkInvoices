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
    internal class InvoiceWatermark
    {
        public PdfTrueTypeFont Font { get; set; } = new PdfTrueTypeFont(new Font("Arial", 20f), true);
        public PdfBrush BackgroundBrush { get; set; } = new PdfSolidBrush(Color.FromArgb(100, Color.Gray));

        public Margins WatermarkMargins { get; set; } = new Margins() { Left = 0, Bottom = 0, Right = 20, Top = 10 };
        public void Watermark(PdfDocument document, string watermark)
        {
            bool hasMultiplePages = document.Pages.Count > 1;
            int pageNumber = 1;
            //Traverse all the pages in the document
            foreach (PdfPageBase page in document.Pages)
            {
                string currentWatermark = watermark;
                if (hasMultiplePages)
                {
                    currentWatermark = $"{watermark} ({pageNumber})";
                }

                SizeF textSize = Font.MeasureString(currentWatermark);
                
                float watermarkX = page.Canvas.Size.Width - textSize.Width + WatermarkMargins.Left - WatermarkMargins.Right;
                float watermarkY = WatermarkMargins.Top - WatermarkMargins.Bottom;
                float backgroundMargin = 2;

                page.Canvas.DrawRectangle(BackgroundBrush, 
                    watermarkX- backgroundMargin, 
                    watermarkY- backgroundMargin, 
                    textSize.Width + backgroundMargin * 2, 
                    textSize.Height + backgroundMargin * 2);

                page.Canvas.DrawString(currentWatermark, Font, PdfBrushes.Black,
                    watermarkX,
                    watermarkY);

                pageNumber++;
            }
        }
    }
}
