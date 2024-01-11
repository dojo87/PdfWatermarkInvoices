using Spire.Pdf.Graphics;
using Spire.Pdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpirePdfWatermarkInvoices
{
    internal class WatermarkKpirPages : IWatermarkDocuments
    {
        public WatermarkKpirPages(string year, KpirWatermark kpirWatermarkWriter) 
        {
            Year = year;
            KpirWatermarkWriter = kpirWatermarkWriter;
        }

        public string Year { get; }
        public KpirWatermark KpirWatermarkWriter { get; }

        public void Watermark(string inDirectory)
        {
            var kpirs = Directory.GetFiles(inDirectory).OrderBy(s => s);

            int currentPage = 1;
            foreach (string kpirPage in kpirs)
            {
                string fileName = Path.GetFileName(kpirPage);
                using (PdfDocument pdf = new PdfDocument())
                {
                    pdf.LoadFromFile(kpirPage);
                    if (pdf.Security.OriginalEncrypt)
                    {
                        Console.WriteLine($"Signed {fileName} - skipping");
                        continue;
                    }

                    Console.Write($"Doing {fileName}");
                    currentPage = KpirWatermarkWriter.WatermarkAndReturnNextPage(pdf, currentPage);

                    string outputPath = Path.Combine(Path.GetDirectoryName(kpirPage), "output", $"{fileName}");
                    //Save the changes to another file
                    pdf.SaveToFile(outputPath);
                    Console.WriteLine($" - DONE");
                }
            }
        }
    }
}
