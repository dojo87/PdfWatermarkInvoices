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
    internal class KpirWatermark
    {
        public PdfTrueTypeFont Font { get; set; } = new PdfTrueTypeFont(new Font("Arial", 20f), true);
        
        public Margins WatermarkMargins { get; set; } = new Margins() { Left = 0, Right = 30, Top = 0, Bottom = 10 };
        public int WatermarkAndReturnNextPage(PdfDocument document, int currentPage)
        {
            foreach (PdfPageBase page in document.Pages)
            {
                
                string currentWatermark = currentPage.ToString();

                SizeF textSize = Font.MeasureString(currentWatermark);
                //KPIR pages are rotated 90deg, we need to rotate the text accordingly.
                page.Canvas.RotateTransform(270, new PointF(0,0));
                page.Canvas.TranslateTransform(-page.Canvas.Size.Height,0);
                // The page now is in the left-top corner of the normal KPIR rotation. 

                float kpirPageWidth = page.Canvas.Size.Height;
                float kpirPageHeight = page.Canvas.Size.Width;

                float watermarkX = kpirPageWidth - WatermarkMargins.Right; // This is a fixed start from the right of the page  - textSize.Width;
                float watermarkY = kpirPageHeight - WatermarkMargins.Bottom - textSize.Height;

                page.Canvas.DrawString(currentWatermark, Font, PdfBrushes.Black, watermarkX, watermarkY);

                currentPage++;
            }

            return currentPage;
        }
    }
}
